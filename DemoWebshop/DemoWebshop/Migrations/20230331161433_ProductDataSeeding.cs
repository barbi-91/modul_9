using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebshop.Migrations
{
    public partial class ProductDataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "InStock", "Price", "Sku", "Title" },
                values: new object[,]
                {
                    { 1, "Ovo vino sa Korčule, punog tijela, jakih voćnih okusa i čvrste strukture, napunjeno je u svega nekoliko stotina buteljki, što objašnjava cijenu višu od Miravalove.", "https://plavakamenica.hr/wp-content/uploads/2019/07/rosei-lista-ljeto-2019.jpg", 5m, 15.50m, "f54ddeda72", "Nerica Crni pošip 2018, Korčula, 90 /100 " },
                    { 2, "Craft lager po originalnoj recepturi od 3 vrste hmelja i 3 vrste ječma, izraženog mirisa i slatkastog okusa.", "https://www.promili.hr/web/image/product.template/2469/image_1024/%5B10664%5D%20Lepi%20De%C4%8Dki%20Tria%20De%20Los%20Muertos?unique=93f7694", 20m, 2.15m, "f54ddeda73", "Lepi Dečki Tria De Los Muertos" },
                    { 3, "Iriedaily Majice kratkih rukava za slobodno vrijeme.", "https://cdn.aboutstatic.com/file/6c84093b0cfbdff00554177c964feb05?brightness=0.96&quality=75&trim=1&height=480&width=360", 3m, 39.90m, "f54ddeda74", "Majica 'Hopi'" },
                    { 4, "Unutrašnjost : Tekstil\r\nGornjište : 27% Sintetika, 73% Tekstil\r\nDonjište : Guma", "https://img.eobuwie.cloud/eob_product_512w_512h(b/7/a/3/b7a3409187765ea0fdfdfa35e5533ead4fcb4b70_01_0000301529996_rz.jpg,jpg)/obuca-nike-air-max-270-gs-wd-do6490-001-black-black-particle-grey.jpg", 2m, 136.57m, "f54ddeda75", "NIKE AIR MAX 270 (GS)" },
                    { 5, "Ako imate mješovitu do masniju kožu, ovo je savršen puder za vas, a sebum vam neće probijati kroz njega. Prekrivna moć je srednja do jača, ali osjećaj na licu je potpuno prirodan. Kožu će vam lagano matirati, ali ako je nakon nanošenja fiksirate transparentnim puderom i onda poprskate mineralnom vodicom za lice, dobit ćete savršeno prirodan look. S obzirom na vrlo pristupačnu cijenu i odličnu kvalitetu, ne čudi da je toliko omiljen.", "https://miss7.24sata.hr/media/img/4b/41/57f9552057adc4c3d7fe.jpeg", 10m, 20.30m, "f54ddeda76", "Catrice HD Liquid Coverage" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
