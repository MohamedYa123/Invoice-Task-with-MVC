using System.Reflection;

namespace Invoice_Task.Database
{

    public class InvoiceView
    {
        public int Id { get; set; }
        public int code { get; set; }
        public DateTime Date { get; set; }
        public string? Customername { get; set; }
        public double Amount { get; set; }
        public List<string> GetAllvalues()
        {
            List<string> values = new List<string>();
            int i = 0;
            foreach (PropertyInfo prop in typeof(InvoiceView).GetProperties())
            {
                if (i == 0)
                {
                    i++;
                    continue;
                }
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                string  value="";
                if (type == typeof(DateTime))
                {
                    value = ((DateTime)prop.GetValue(this, null)).ToString("dd/MM/yyyy");
                }
                else
                {
                    value = prop.GetValue(this, null) + "";
                }
                values.Add(value);
                i++;
            }

            return values;
        }
    }

}
