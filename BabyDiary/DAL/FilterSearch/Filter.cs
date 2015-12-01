
namespace BabyDiary.DAL.FilterSearch
{
    public class Filter
    {
        public Filter(string propertyName, object value, Op operation = Op.Equals)
        {
            PropertyName = propertyName;
            Value = value;
            Operation = operation;
        }
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }
}