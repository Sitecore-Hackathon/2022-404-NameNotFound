using Sitecore.Data;

namespace BasicCompany.Feature.BasicContent
{
    public static class Templates
    {
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

        public static class SectionHeader
        {
            public static class Fields
            {
                public static readonly ID Text = new ID("{59D24D0A-F955-4988-B26F-92039B4DF8BD}");
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

        public static class AccordionItem
        {
            public static class Fields
            {
                public static readonly ID Title = new ID("{5718E787-142B-41D9-B5A1-0B18F45B8236}");
                public static readonly ID Content = new ID("{45EFE66E-5AD2-4F1D-BAD5-FDF281688681}");
            }
        }
    }
}