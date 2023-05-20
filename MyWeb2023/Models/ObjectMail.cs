using Myweb.Domain.Models.Entities;

namespace MyWeb2023.Models
{
    public class ObjectMail
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ObjectMail(string key ,string value)
        {
            Key = key;
            Value = value;
        }

        public ObjectMail()
        {
        }
    }
}
