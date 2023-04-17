namespace Pdf2Image.Import.Regions
{
    public static class SpvRegions
    {
        public static Rectangle GetBankName() => new(2100, 390, 400, 65);

        public static Rectangle GetBrandName() => new(1600, 540, 250, 35);

        public static Rectangle GetDateRegion() => new(310, 0, 170, 0);

        public static Rectangle GetArsRegion() => new(1830, 0, 300, 0);

        public static Rectangle GetUsdRegion() => new(2200, 0, 300, 0);
    }
}