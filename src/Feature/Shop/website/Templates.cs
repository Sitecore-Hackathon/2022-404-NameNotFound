using Sitecore.Data;

namespace Tshirts.Feature.shop
{
    public static class Templates
    {

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
                public static readonly ID Id = new ID("{8E8D301B-7193-4AF6-8FC3-5F8416D86B04}");
                public static readonly ID Name = new ID("{71B6EB50-C4A9-44FF-A10D-D723BF0F3F5A}");
                public static readonly ID BrandName = new ID("{55EE9073-133C-4C6D-A2B2-036F3BAEC15D}");
                public static readonly ID Color = new ID("{1972BBAA-CCA6-4C3B-B063-DB4FE12F7170}");
                public static readonly ID Size = new ID("{81BD668F-2C8E-4E5D-874D-A8A15E9EA911}");
                public static readonly ID Description = new ID("{0D8FE3FA-D156-4D92-86AD-DB830C15D443}");
                public static readonly ID Material = new ID("{21FC5B97-8B37-4C40-BEF1-6ACA561721BE}");
                public static readonly ID Manufacturer = new ID("{930AFB19-F797-4B60-91B3-98E96E8A77EF}");
                public static readonly ID Price = new ID("{73F3DB53-17DA-4B7C-B23C-972B2C37E6A4}");
                public static readonly ID Images = new ID("{C5A3D137-C3E1-48A3-B722-4C643522B44A}");
                public static readonly ID Inventory = new ID("{704A16CE-B7EC-479A-BE27-A678F1167CD3}");
            }
        }

        public static class BasicContent
        {
            public static class Fields
            {
                public static readonly ID HeroTitle = new ID("{DFBE4B2A-1ECD-4A1D-81A4-6901073624DA}");
                public static readonly ID ProductSectionTitle = new ID("{2FC32E94-89C7-4132-AAFE-71EB6731F53D}");
                public static readonly ID ProductSectionDescription = new ID("{8610C8F2-33AF-42CC-BDE8-5ADAC144CCBF}");
                public static readonly ID ProductSectionList = new ID("{B4AF77E7-682F-43CF-AC15-36EAAC270D9F}");
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