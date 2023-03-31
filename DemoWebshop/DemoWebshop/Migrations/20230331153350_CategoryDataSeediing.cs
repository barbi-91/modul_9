using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebshop.Migrations
{
    public partial class CategoryDataSeediing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Image", "Title" },
                values: new object[,]
                {
                    { 1, "Rose Dea Pannonium dobiva se od sorte Pinot crni, nježne je ružičaste boje, raskošnih voćnih aroma, mekano i skladno. Vino koje će zadovoljiti ljubitelje slatkastih, sočnih i voćnih rose vina.", "https://webshop.rotodinamic.hr/image/cache/cache/2001-3000/2434/main/c826-022155-0-2-800x1200.jpg", "Rose Dea Pannonium" },
                    { 2, "U svijetu se najviše konzumiraju tzv. lager piva ili “piva donjeg vrenja” koja se dobivaju vrenjem pivske sladovine pomoću različitih sojeva čiste kulture kvasca vrste Saccharomyces uvarum.", "https://www.beerstyle.rs/wp-content/uploads/2018/07/lager_beerstyle_07_1499-1024x767.jpg", "Lager pivo" },
                    { 3, "Odjeća za slobodno vrijeme je odličan izbor kada osim atraktivnom izgleda trebaš i udobnost.", "https://cdn.nexgeontools.com/ml2PH3yIFr8frQ6wnRUB4_Pb0Qc=/fit-in/0x0/nexgeontools%2Fcreatives%2F2023%2F03%2Fodjeca-za-slobodno-vrijeme-vk38-hrny7v%2Fimages%2F1%2Fimg_64117bc8096e21.96924669.png", "Odjeca za slobodno vrijeme" },
                    { 4, "Požurite po svoju sportsku kombinaciju i budite u trendu po povoljnim cijenama.", "https://www.restoranloncic.com.hr/upload/1-cdn_127077/Mu%C5%A1ka-obu%C4%87a-za-tenis-%D0%BF%D0%B0%D1%80%D1%83%D1%81%D0%B8%D0%BD%D0%BE%D0%B2%D0%B0%D1%8F-sportska.jpg", "Sportska obuca" },
                    { 5, "Istražite svijet kozmetičkih proizvoda i uživajte u ekskluzivnim ponudama.", "https://krenizdravo.dnevnik.hr/wp-content/uploads/2017/09/mineralna-kozmetika.jpg?x11092", "Puder" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
