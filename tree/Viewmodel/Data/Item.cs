namespace tree{
    public class Item 
    {
       // public static bool selected { get; set; }

        public int Img { get; set; }

        public int Txt { get; set; }

        public int Zip { get; set; }

        public int Other { get; set; }

        public int Pdf { get; set; }

        public static Item operator+ (Item a,Item b){
            Item c = new Item
            {
                Img = a.Img + b.Img,
                Txt = a.Txt + b.Txt,
                Zip = a.Zip + b.Zip,
                Other = a.Other + b.Other,
                Pdf = a.Pdf + b.Pdf
            };
            return c;
        }
    }
}
