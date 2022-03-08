using MassTransit;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using TestPackages.Messages;

namespace TestPackages.MassTransit.Helpers
{
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Creates command type with CommandId and CorrelationId
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        internal static (object command, Guid commandId) CreateCommandMessage<TCommand>(this object values)
            where TCommand : class, ICommand
        {
            var type = DefineMessageType<TCommand>();
            var obj = Activator.CreateInstance(type);

            var commandId = Guid.Empty;
            var hasCommandId = false;
            var hasCorrelationId = false;

            foreach (var prop in type.GetProperties())
            {
                var valueProp = values.GetType().GetProperty(prop.Name);

                if (valueProp != null)
                {
                    var value = valueProp.GetValue(values, null);
                    prop.SetValue(obj, value, null);

                    switch (prop.Name)
                    {
                        case "CommandId":
                            hasCommandId = true;
                            break;
                        case "CorrelationId":
                            hasCorrelationId = true;
                            break;
                        default:
                            break;
                    }

                    if (commandId == Guid.Empty && prop.Name.EndsWith("Id") && value != null &&
                        (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?)))
                    {
                        commandId = (Guid)value;
                    }
                }
            }

            if (commandId == Guid.Empty)
            {
                commandId = NewId.NextGuid();
            }

            if (!hasCommandId)
                obj.GetType().GetProperty("CommandId").SetValue(obj, commandId, null);
            if (!hasCorrelationId)
                obj.GetType().GetProperty("CorrelationId").SetValue(obj, commandId, null);

            return (obj, commandId);
        }

        /// <summary>
        /// Creates event type with CorrelationId
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="values"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static (object @event, Guid correlationId) CreateEventMessage<TEvent>(this object values, ConsumeContext context)
            where TEvent : class, IEvent
        {
            var type = DefineMessageType<TEvent>();
            var obj = Activator.CreateInstance(type);

            var hasCorrelationId = false;

            foreach (var prop in type.GetProperties())
            {
                var valueProp = values.GetType().GetProperty(prop.Name);

                if (valueProp != null)
                {
                    var value = valueProp.GetValue(values, null);
                    prop.SetValue(obj, value, null);

                    if (prop.Name == "CorrelationId")
                    {
                        hasCorrelationId = true;
                    }
                }
            }

            var correlationId = NewId.NextGuid();

            if (context.CorrelationId.HasValue)
                correlationId = context.CorrelationId.Value;

            if (!hasCorrelationId)
                obj.GetType().GetProperty("CorrelationId").SetValue(obj, correlationId, null);

            return (obj, correlationId);
        }

        private static Type DefineMessageType<TMessage>() where TMessage : class
        {
            AssemblyName assemblyName = new AssemblyName(typeof(TMessage).FullName);
            AssemblyBuilder assemblyBuilder =
                AssemblyBuilder.DefineDynamicAssembly(
                    assemblyName,
                    AssemblyBuilderAccess.Run);

            ModuleBuilder ModuleBuilder =
                assemblyBuilder.DefineDynamicModule(assemblyName.Name);

            TypeBuilder typeBuilder = ModuleBuilder.DefineType(
                typeof(TMessage).Name,
                TypeAttributes.Public);

            var properties = typeof(TMessage)
                     .GetProperties()
                     .Union(typeof(TMessage)
                                .GetInterfaces()
                                .SelectMany(t => t.GetProperties()));

            foreach (var property in properties)
            {
                DefineMessageProperties(typeBuilder, property.Name, property.PropertyType);
            }

            return typeBuilder.CreateType();
        }

        private static void DefineMessageProperties(this TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField(
                $"m_{propertyName.ToLower()}",
                propertyType,
                FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(
                propertyName,
                PropertyAttributes.HasDefault,
                propertyType,
                null);

            MethodAttributes getSetAttr = MethodAttributes.Public |
                MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            MethodBuilder methodBuilderGetAccessor = tb.DefineMethod(
                $"get_{propertyName}",
                getSetAttr,
                propertyType,
                Type.EmptyTypes);

            ILGenerator getIL = methodBuilderGetAccessor.GetILGenerator();

            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            MethodBuilder methodBuilderSetAccessor = tb.DefineMethod(
                $"set_{propertyName}",
                getSetAttr,
                null,
                new Type[] { propertyType });

            ILGenerator setIL = methodBuilderSetAccessor.GetILGenerator();

            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(methodBuilderGetAccessor);
            propertyBuilder.SetSetMethod(methodBuilderSetAccessor);
        }
    }
}