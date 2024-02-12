namespace WpfApp.DeviceUnit
{
    internal class LED
    {
        public bool Red { get; set; } = false;
        public bool Green { get; set; } = false;
        public bool Blue { get; set; } = false;

        public string ToMessage()
        {
            var mes = "";
            if (Red)
            {
                mes += "xr";
            }
            if (Green)
            {
                mes += "xg";
            }
            if (Blue)
            {
                mes += "xb";
            }
            if (!Red && !Green && !Blue)
            {
                mes += "xc";
            }
            return mes;
        }
    }
}
