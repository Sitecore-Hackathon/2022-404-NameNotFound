using Sitecore.Data;

namespace Tshirts.Feature.shop
{
    public static class Templates
    {
        public static class PromoCard
        {
            public static class Fields
            {
                public static readonly ID Link = new ID("{B788E8BC-E944-4A2B-A4BE-3424643D322B}");
                public static readonly ID Image = new ID("{21249F44-5F0F-4CFA-9474-8D72930D6575}");
                public static readonly ID Headline = new ID("{4F73C02B-93CC-4C96-B9FF-9D0D8E853ED6}");
                public static readonly ID Text = new ID("{13EB8DCA-281D-4E75-B6E1-701CA719BCD1}");
            }
        }

        public static class Header
        {
            public static class Fields
            {
                public static readonly ID Text = new ID("{59D24D0A-F955-4988-B26F-92039B4DF8BD}");
            }
        }

        public static class Product
        {
            public static class Fields
            {
                public static readonly ID Name = new ID("{71B6EB50-C4A9-44FF-A10D-D723BF0F3F5A}");
                public static readonly ID Id = new ID("{8E8D301B-7193-4AF6-8FC3-5F8416D86B04}");
                public static readonly ID BrandName = new ID("{55EE9073-133C-4C6D-A2B2-036F3BAEC15D}");
                public static readonly ID Color = new ID("{1972BBAA-CCA6-4C3B-B063-DB4FE12F7170}");
                public static readonly ID Size = new ID("{81BD668F-2C8E-4E5D-874D-A8A15E9EA911}");
                public static readonly ID Description = new ID("{0D8FE3FA-D156-4D92-86AD-DB830C15D443}");
                public static readonly ID Material = new ID("{21FC5B97-8B37-4C40-BEF1-6ACA561721BE}");
                public static readonly ID Manufacturer = new ID("{930AFB19-F797-4B60-91B3-98E96E8A77EF}");
            }
        }

        public static class HeroBanner
        {
            public static class Fields
            {
                public static readonly ID Title = new ID("{5179186C-B95E-4E97-95AB-7958721A9AEB}");
                public static readonly ID Subtitle = new ID("{89B0A8ED-0EE8-4512-B518-AB2C4C2A0B9E}");
                public static readonly ID Image = new ID("{B5F61442-FF0F-46A5-90A8-D6D387DE24A0}");
            }
        }

    }
}