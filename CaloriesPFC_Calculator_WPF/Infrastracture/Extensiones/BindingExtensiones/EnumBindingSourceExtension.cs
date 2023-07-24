using System;
using System.Windows.Markup;

namespace CaloriesPFC_Calculator_WPF.Infrastracture.Extensiones.BindingExtensiones
{
    internal class EnumBindingSourceExtension : MarkupExtension
    {
        public Type EnumType { get; private set; }
        public EnumBindingSourceExtension(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
                throw new Exception("parameter enumType must not be null and of type Enum");
            EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
