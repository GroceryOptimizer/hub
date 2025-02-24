using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Sku = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    GrpcAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    StoreId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => new { x.StoreId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_Inventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inventories_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Description", "Image", "Name", "Sku" },
                values: new object[,]
                {
                    { new Guid("0481a834-44bb-4298-807e-e3ddcef6481b"), "Ramlösa", "Mineralvatten", "Ett naturligt mineralvatten med kolsyra.", "https://www.example.com/ramlosa_vatten.jpg", "Ramlösa Mineralvatten", "ramlosa_vatten" },
                    { new Guid("0d1e2f3a-4b5c-6d7e-8f9a-0b1c2d3e4f5a"), "Barilla", "Pasta", "Klassisk italiensk pasta, perfekt för alla pastarätter.", "https://www.example.com/barilla_pasta.jpg", "Pasta", "barilla_pasta" },
                    { new Guid("19dd4653-0075-43fd-afe6-591692d2ded5"), "Viking", "Mjöl", "Ett svenskt mjöl som är lämpligt för bakning.", "https://www.example.com/viking_mjol.jpg", "Viking Mjöl", "viking_mjol" },
                    { new Guid("1a75693b-1d08-43f7-b4b9-18da3e0974b0"), "Kockens", "Kryddor", "Ett brett utbud av kryddor för matlagning.", "https://www.example.com/kockens_kryddor.jpg", "Kockens Kryddor", "kockens_kryddor" },
                    { new Guid("1b2c3d4e-5f6a-7b8c-9d0e-1f2a3b4c5d6e"), "Felix", "Sylt", "Klassisk svensk lingonsylt, perfekt till pannkakor eller köttbullar.", "https://www.example.com/felix_lingonsylt.jpg", "Lingonsylt", "felix_lingonsylt" },
                    { new Guid("2a3b4c5d-6e7f-8a9b-0c1d-2e3f4a5b6c7d"), "Pågen", "Bröd", "Mörkt och smakrikt limpa bröd, perfekt till frukost.", "https://www.example.com/pagen_limpa.jpg", "Pågenlimpa", "pagen_limpa" },
                    { new Guid("36612848-7709-4821-99b7-ebb25ddfc679"), "Lipton", "Drycker", "Ett svart te med en frisk och uppiggande smak.", "https://www.example.com/lipton_te.jpg", "Te", "lipton_te" },
                    { new Guid("3c4d5e6f-7a8b-9c0d-1e2f-3a4b5c6d7e8f"), "Lurpak", "Mejeri", "Kvalitetssmör från Lurpak, perfekt för bakning och matlagning.", "https://www.example.com/lurpak_smor.jpg", "Smör", "lurpak_smor" },
                    { new Guid("3cb591c8-d3ed-4a3c-a26a-698167c73be1"), "Skansen", "Senap", "En klassisk svensk senap med en mild och söt smak.", "https://www.example.com/skansen_senap.jpg", "Skansen Senap", "skansen_senap" },
                    { new Guid("45eea981-3a74-41bb-b5bb-c55ce2078c46"), "Gevalia", "Drycker", "Ett mörkrostad kaffe med en rik och aromatisk smak.", "https://www.example.com/gevalia_kaffe.jpg", "Kaffe", "gevalia_kaffe" },
                    { new Guid("45f4ef7a-d58f-455d-a651-48d1087f0791"), "Arla", "Mejeri", "En syrlig mejeriprodukt som används i matlagning.", "https://www.example.com/arla_graddfil.jpg", "Gräddfil", "arla_graddfil" },
                    { new Guid("4d5e5b20-1885-4279-bff8-ef730ff289f0"), "Findus", "Grönsaker", "Frysta ärtor av hög kvalitet, perfekta som tillbehör till middagen.", "https://www.example.com/findus_frysta_artor.jpg", "Frysta Ärtor", "findus_frysta_artor" },
                    { new Guid("4d5e6f7a-8b9c-0d1e-2f3a-4b5c6d7e8f9a"), "Scan", "Kött", "En svensk klassiker, perfekt för stekning eller grillning.", "https://www.example.com/scan_falukorv.jpg", "Falukorv", "scan_falukorv" },
                    { new Guid("5348eafe-24e3-4679-b78a-034b32abb48f"), "Risenta", "Gryn", "Olika typer av gryn, som till exempel havregryn eller korngryn.", "https://www.example.com/risenta_gryn.jpg", "Risenta Gryn", "risenta_gryn" },
                    { new Guid("59b8c04e-47dd-4fca-8659-9499d8e422fa"), "Marabou", "Godis", "En mjölkchoklad med en krämig och söt smak.", "https://www.example.com/marabou_choklad.jpg", "Choklad", "marabou_choklad" },
                    { new Guid("5e6f7a8b-9c0d-1e2f-3a4b-5c6d7e8f9a0b"), "Pepsi", "Läsk", "En sockerfri läsk med en fräsch smak.", "https://www.example.com/pepsi_max.jpg", "Pepsi Max", "pepsi_max" },
                    { new Guid("6750ba42-3371-4753-82c0-1a4e5344ade2"), "Marabou", "Choklad", "En klassisk svensk mjölkchokladkaka.", "https://www.example.com/cloetta_marabou.jpg", "Marabou Mjölkchoklad", "cloetta_marabou" },
                    { new Guid("6f7a8b9c-0d1e-2f3a-4b5c-6d7e8f9a0b1c"), "Leksands", "Bröd", "Tunnbröd från Leksands, perfekt för smörgåsar eller mackor.", "https://www.example.com/leksands_tunnbrod.jpg", "Tunnbröd", "leksands_tunnbrod" },
                    { new Guid("7033ae51-569f-418f-bc8a-4d2e688ab179"), "Wasa", "Knäckebröd", "Ett tunt och krispigt knäckebröd, perfekt till frukost eller mellanmål.", "https://www.example.com/wasa_knackebrod.jpg", "Wasa Knäckebröd", "wasa_knackebrod" },
                    { new Guid("7323bcfb-1daa-41aa-9e53-816011102ac6"), "Abba", "Fiskkonserver", "Klassisk svensk matjessill, perfekt till midsommar eller andra högtider.", "https://www.example.com/abba_matjessill.jpg", "Matjessill", "abba_matjessill" },
                    { new Guid("78e46498-c10c-408a-b3c3-5a3e5684abec"), "Sveriges Bästa Ägg", "Ägg", "Färska ägg från svenska gårdar.", "https://www.example.com/sveriges_basta_agg.jpg", "Ägg", "sveriges_basta_agg" },
                    { new Guid("7a8b9c0d-1e2f-3a4b-5c6d-7e8f9a0b1c2d"), "Green Giant", "Grönsaker", "Kronärtskockshjärta i vatten, perfekt för sallader eller tillbehör.", "https://www.example.com/green_giant_kronartskock.jpg", "Kronärtskockshjärta", "green_giant_kronartskock" },
                    { new Guid("7eec1f41-ecc1-4659-ad6f-e571f97076be"), "Oreo", "Godis", "Klassiska kex med en krämig vaniljfyllning.", "https://www.example.com/oreo_kex.jpg", "Kex", "oreo_kex" },
                    { new Guid("8926a5a1-437c-4ebd-b331-e6a9bfa02c5b"), "Santa Maria", "Tex Mex", "Krispiga tacoskal, perfekta för att göra tacos.", "https://www.example.com/santa_maria_tacoskal.jpg", "Tacoskal", "santa_maria_tacoskal" },
                    { new Guid("8a8db6dc-73ad-45fa-818f-697094c1cc60"), "Falu Rödfärg", "Färg", "En traditionell svensk färg som används för att måla hus.", "https://www.example.com/falu_rod_farg.jpg", "Falu Rödfärg", "falu_rod_farg" },
                    { new Guid("8b9c0d1e-2f3a-4b5c-6d7e-8f9a0b1c2d3e"), "Felix", "Sallad", "En söt och syrlig rödbetssallad, perfekt till kötträtter.", "https://www.example.com/felix_rodbetssallad.jpg", "Rödbetssallad", "felix_rodbetssallad" },
                    { new Guid("8c7f6994-15a4-4183-8752-27ab45a2f13c"), "Lurpak", "Mejeri", "Ett smör med en rik och krämig smak, perfekt för matlagning och bakning.", "https://www.example.com/lurpak_smor.jpg", "Smör", "lurpak_smor" },
                    { new Guid("8d4667ea-d6b4-4252-a0ee-351737007d06"), "GB Glace", "Glass", "Krämig vaniljglass, en favorit hos många.", "https://www.example.com/gb_glass_vanilj.jpg", "Vaniljglass", "gb_glass_vanilj" },
                    { new Guid("9546816f-4bef-4772-b098-01d440d92964"), "Uncle Ben's", "Torrvaror", "Ett långkornigt ris som är perfekt för alla typer av maträtter.", "https://www.example.com/uncle_bens_ris.jpg", "Ris", "uncle_bens_ris" },
                    { new Guid("99b3e53c-efe0-456c-89ce-e4f42baef09a"), "Kalles", "Fiskpålägg", "En svensk klassiker, krämig och smakrik kaviar gjord på torskrom.", "https://www.example.com/kalles_kaviar.jpg", "Kalles Kaviar", "kalles_kaviar" },
                    { new Guid("9c0d1e2f-3a4b-5c6d-7e8f-9a0b1c2d3e4f"), "Gevalia", "Kaffe", "En rik och aromatisk kaffe, perfekt för morgonen.", "https://www.example.com/gevalia_kaffe.jpg", "Kaffe", "gevalia_kaffe" },
                    { new Guid("a0b1c2d3-e4f5-6789-0123-def012345678"), "Felix", "Kryddor", "En svensk ketchup med en söt och syrlig smak.", "https://www.example.com/felix_ketchup.jpg", "Ketchup", "felix_ketchup" },
                    { new Guid("a135bd8a-755e-459c-81f2-2e42c9aed909"), "Estrella", "Snacks", "Ett saltat popcorn med en lätt och luftig konsistens.", "https://www.example.com/estrella_popcorn.jpg", "Popcorn", "estrella_popcorn" },
                    { new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"), "Fazer", "Bröd", "Ett smakrikt rågbröd med en mjuk och saftig konsistens.", "https://www.example.com/fazer_ragbrod.jpg", "Rågbröd", "fazer_ragbrod" },
                    { new Guid("a4b5c6d7-e8f9-0123-4567-890abcdef012"), "Kikkoman", "Asiatiska ingredienser", "En japansk sojasås med en rik och fyllig smak.", "https://www.example.com/kikkoman_soja.jpg", "Soja", "kikkoman_soja" },
                    { new Guid("a6b7c8d9-e0f1-3456-7890-34567890abcd"), "St Michel", "Frukost", "En fransk marmelad med en smak av apelsin och citron.", "https://www.example.com/st_michel_marmelad.jpg", "Marmelad", "st_michel_marmelad" },
                    { new Guid("a7b8c9d0-e1f2-3456-7890-abcdefabcdef"), "Lambi", "Hygien och rengöring", "Mjukt och skönt toalettpapper.", "https://www.example.com/lambi_toalettpapper.jpg", "Toalettpapper", "lambi_toalettpapper" },
                    { new Guid("b031364e-3453-4ccd-b1e5-6c0918a54db1"), "Polarbröd", "Bröd", "Mjukt och smakrikt bröd från Hönö.", "https://www.example.com/polar_brod_honokaka.jpg", "Hönökaka", "polar_brod_honokaka" },
                    { new Guid("b18bdff4-1e60-42c5-8ae0-92b62513ece1"), "Arla", "Mejeri", "Färsk mjölk med hög kvalitet, perfekt för frukost eller bakning.", "https://www.example.com/arla_mjolk.jpg", "Mjölk", "arla_standard_mjolk" },
                    { new Guid("b1c2d3e4-f5a6-7890-1234-ef0123456789"), "Hellmann's", "Kryddor", "En krämig majonnäs med en rik och fyllig smak.", "https://www.example.com/hellmanns_majonnas.jpg", "Majonnäs", "hellmanns_majonnas" },
                    { new Guid("b2c3d4e5-f6a7-8901-2345-67890abcdefa"), "ICA", "Kött", "Färdiga köttbullar, perfekta till middagen eller som tilltugg.", "https://www.example.com/ica_kottbullar.jpg", "Köttbullar", "ica_kottbullar" },
                    { new Guid("b5c6d7e8-f9a0-1234-5678-90abcdef0123"), "Santa Maria", "Kryddor", "En stark chilisås med en intensiv smak.", "https://www.example.com/santa_maria_chili.jpg", "Chili", "santa_maria_chili" },
                    { new Guid("b7ad2594-cf46-4328-be12-6b37b42d52ee"), "Arla", "Mjölk", "Svensk mellanmjölk med 1,5% fett.", "https://www.example.com/arla_mjolk.jpg", "Mellanmjölk", "arla_mjolk" },
                    { new Guid("b8c9d0e1-f2a3-4567-8901-bcdefabcdef0"), "Oral-B", "Hygien och rengöring", "En eltandborste som ger en effektiv rengöring.", "https://www.example.com/oralb_tandborste.jpg", "Tandborste", "oralb_tandborste" },
                    { new Guid("bbf52a56-6a7f-42ca-adae-b41db4345d80"), "Kavli", "Ost", "Krämig mjukost med olika smaker.", "https://www.example.com/kavli_ost.jpg", "Mjukost", "kavli_ost" },
                    { new Guid("bf6b70d3-5ff9-4bfe-babb-dd2a5ab315d4"), "Zoégas", "Kaffe", "Mörkrostat kaffe med en fyllig och kraftig smak.", "https://www.example.com/zoegas_skanerost.jpg", "Skånerost", "zoegas_skanerost" },
                    { new Guid("c2d3e4f5-a6b7-8901-2345-f01234567890"), "Felix", "Kryddor", "En dansk remouladsås med en söt, syrlig och kryddig smak.", "https://www.example.com/felix_remouladsas.jpg", "Remouladsås", "felix_remouladsas" },
                    { new Guid("c3d4e5f6-a7b8-9012-3456-7890abcdefab"), "Coop", "Frukt och grönt", "Färska blåbär, rika på antioxidanter och smak.", "https://www.example.com/coop_blabar.jpg", "Blåbär", "coop_blabar" },
                    { new Guid("c61a046b-e3ef-4603-ad60-a0879c9151c7"), "OLW", "Snacks", "Ett saltade chips med en krispig och smakrik konsistens.", "https://www.example.com/olw_chips.jpg", "Chips", "olw_chips" },
                    { new Guid("c6d7e8f9-a0b1-2345-6789-0abcdef01234"), "Biodlingarna", "Honung", "En svensk honung med en söt och mild smak.", "https://www.example.com/biodlingarna_honung.jpg", "Honung", "biodlingarna_honung" },
                    { new Guid("c944f005-afcd-4498-8703-6635ba0dd95c"), "Kronägg", "Mejeri", "Färska ägg från frigående höns, perfekta för frukost eller bakning.", "https://www.example.com/kronagg_agg.jpg", "Ägg", "kronagg_agg" },
                    { new Guid("c9d0e1f2-a3b4-5678-9012-cdefabcdef01"), "Head & Shoulders", "Hårvård", "Ett schampo som motverkar mjäll och ger en frisk hårbotten.", "https://www.example.com/head_shoulders_schampo.jpg", "Schampo", "head_shoulders_schampo" },
                    { new Guid("d0e1f2a3-b4c5-6789-0123-defabcdef012"), "L'Oréal", "Hårvård", "Ett balsam som gör håret mjukt och lätt att reda ut.", "https://www.example.com/loreal_balsam.jpg", "Balsam", "loreal_balsam" },
                    { new Guid("d3e4f5a6-b7c8-9012-3456-01234567890a"), "Podravka", "Kryddor", "En smakrik relish gjord på paprika, aubergine och chili.", "https://www.example.com/podravka_ajvar.jpg", "Ajvar Relish", "podravka_ajvar" },
                    { new Guid("d4e5f6a7-b8c9-0123-4567-890abcdefabc"), "Arla", "Mejeri", "En naturell yoghurt utan tillsatt socker.", "https://www.example.com/arla_yoghurt_naturell.jpg", "Yoghurt Naturell", "arla_yoghurt_naturell" },
                    { new Guid("d7e082ff-195e-4b5e-8f1b-5f503f90d1f9"), "Potatis King", "Potatis", "En mjölig potatissort som är populär i Sverige.", "https://www.example.com/potatis_king_edward.jpg", "King Edward Potatis", "potatis_king_edward" },
                    { new Guid("d7e8f9a0-b1c2-3456-7890-abcdef012345"), "Falksalt", "Kryddor", "Ett havssalt med en ren och naturlig smak.", "https://www.example.com/falksalt_salt.jpg", "Salt", "falksalt_salt" },
                    { new Guid("e2f3a4b5-c6d7-8901-2345-67890abcdef0"), " Zeta", "Matolja", "En extra jungfru olivolja av hög kvalitet.", "https://www.example.com/zeta_olivolja.jpg", "Olivolja", "zeta_olivolja" },
                    { new Guid("e4f5a6b7-c8d9-0123-4567-1234567890ab"), "Barilla", "Pasta", "En italiensk pesto med en smak av basilika, pinjenötter och parmesanost.", "https://www.example.com/barilla_pesto.jpg", "Pesto", "barilla_pesto" },
                    { new Guid("e5f6a7b8-c9d0-1234-5678-90abcdefabcd"), "Andersson", "Köksredskap", "En praktisk osthyvel för att skiva ost.", "https://www.example.com/andersson_osthyvel.jpg", "Osthyvel", "andersson_osthyvel" },
                    { new Guid("e8f9a0b1-c2d3-4567-8901-bcdef0123456"), "Santa Maria", "Kryddor", "En svartpeppar med en stark och aromatisk smak.", "https://www.example.com/santa_maria_peppar.jpg", "Peppar", "santa_maria_peppar" },
                    { new Guid("f3a4b5c6-d7e8-9012-3456-7890abcdef01"), "Kung Markatta", "Vinäger", "En ekologisk äppelcidervinäger.", "https://www.example.com/kung_markatta_vinager.jpg", "Vinäger", "kung_markatta_vinager" },
                    { new Guid("f5a6b7c8-d9e0-2345-6789-234567890abc"), "Nicolas Vahé", "Delikatesser", "En fransk tapenade gjord på oliver, kapris och ansjovis.", "https://www.example.com/nicolas_vahe_tapenade.jpg", "Tapenade", "nicolas_vahe_tapenade" },
                    { new Guid("f6a7b8c9-d0e1-2345-6789-0abcdefabcde"), "Yes", "Hygien och rengöring", "Effektivt diskmedel som tar bort fett och smuts.", "https://www.example.com/yes_diskmedel.jpg", "Diskmedel", "yes_diskmedel" },
                    { new Guid("f9a0b1c2-d3e4-5678-9012-cdef01234567"), "Slotts", "Kryddor", "En svensk senap med en mild och söt smak.", "https://www.example.com/slotts_senap.jpg", "Senap", "slotts_senap" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventories",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
