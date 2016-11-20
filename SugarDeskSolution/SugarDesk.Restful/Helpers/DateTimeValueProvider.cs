namespace SugarDesk.Restful.Helpers
{
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateTimeValueProvider : IValueProvider
    {
        private readonly IValueProvider Provider;

        public DateTimeValueProvider(IValueProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            Provider = provider;
        }

        public object GetValue(object target)
        {
            return Provider.GetValue(target);
        }

        public void SetValue(object target, object value)
        {
            if (value == null)
            {
                Provider.SetValue(target, null);
            }
            Provider.SetValue(target, value);
        }
    }
}
