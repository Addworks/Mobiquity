namespace com.mobiquity.packer
{
    public record PackageItem()
    {
        public int ElementIndex { get; init; }
        public double Weight { get; init; }
        public double Cost { get; init; }

        public static implicit operator PackageItem(string v)
        {
            var p = v.Replace("€", "").Replace("(", "").Replace(")", "").Split(',');
            return new PackageItem { ElementIndex = Convert.ToInt32(p[0]), Weight = Convert.ToDouble(p[1].Replace(".", ",")), Cost = Convert.ToDouble(p[2]) };
        }
    }
}