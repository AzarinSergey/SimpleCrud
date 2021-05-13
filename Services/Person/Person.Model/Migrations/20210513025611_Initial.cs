using Microsoft.EntityFrameworkCore.Migrations;

namespace Person.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Person",
                columns: new[] { "Id", "City", "Email", "FirstName", "LastName", "PhoneNumber", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 1, "г Новосибирск", "Bonnie_Bode83@yahoo.com", "Игнат", "Лебедев", "79166113768", "17 Bromwell Street", "11791" },
                    { 73, "г Новосибирск", "Chance_Blanda74@hotmail.com", "Алексей", "Поливанов", "79199629371", "17 Bromwell Street", "48601" },
                    { 72, "г Новосибирск", "Anabelle_OConnell@hotmail.com", "Августиния", "Авилова", "79249669496", "17 Bromwell Street", "46304" },
                    { 71, "г Новосибирск", "Barton_Labadie@yahoo.com", "Артем", "Крылов", "79248643064", "17 Bromwell Street", "11791" },
                    { 70, "г Новосибирск", "Abel_Rolfson38@gmail.com", "Георгий", "Косыгина", "79274932037", "17 Bromwell Street", "30120" },
                    { 69, "г Новосибирск", "Bonnie_Bode83@yahoo.com", "Агния", "Булыгина", "79251390755", "17 Bromwell Street", "30120" },
                    { 68, "г Новосибирск", "Deron71@hotmail.com", "Роман", "Железнов", "79301115351", "17 Bromwell Street", "46304" },
                    { 67, "г Новосибирск", "Anna24@hotmail.com", "Герман", "Ялунина", "79235970418", "17 Bromwell Street", "46304" },
                    { 66, "г Новосибирск", "Demarco_Schulist39@hotmail.com", "Сергей", "Крылов", "79122253313", "17 Bromwell Street", "30120" },
                    { 65, "г Новосибирск", "Allie.Thompson24@gmail.com", "Артур", "Крылов", "79222998760", "17 Bromwell Street", "48601" },
                    { 64, "г Новосибирск", "Dayna38@gmail.com", "Авагея", "Яковец", "79261414348", "17 Bromwell Street", "30120" },
                    { 63, "г Новосибирск", "Frankie.Sporer@yahoo.com", "Рената", "Прудников", "79261414348", "17 Bromwell Street", "48601" },
                    { 62, "г Новосибирск", "Bethel92@gmail.com", "Захар", "Бурцева", "79263258303", "17 Bromwell Street", "11791" },
                    { 61, "г Новосибирск", "Anabelle_OConnell@hotmail.com", "Вадим", "Сыромятникова", "79177725196", "17 Bromwell Street", "30120" },
                    { 60, "г Новосибирск", "Clifford23@hotmail.com", "Алексей", "Антимонова", "79147692876", "17 Bromwell Street", "48601" },
                    { 59, "г Новосибирск", "Edyth24@yahoo.com", "Абдупатта", "Косыгина", "79297956774", "17 Bromwell Street", "48601" },
                    { 58, "г Новосибирск", "Arnaldo14@gmail.com", "Семен", "Бурцев", "79274148070", "17 Bromwell Street", "46304" },
                    { 57, "г Новосибирск", "Cristal_Dare97@gmail.com", "Доминика", "Фамусов", "79194399119", "17 Bromwell Street", "11791" },
                    { 56, "г Новосибирск", "Christian_Kirlin47@yahoo.com", "Камиль", "Немцов", "79197853566", "17 Bromwell Street", "30120" },
                    { 55, "г Новосибирск", "Billie_Ankunding61@yahoo.com", "Агния", "Ермолаева", "79301011718", "17 Bromwell Street", "46304" },
                    { 54, "г Новосибирск", "Electa.Kris@yahoo.com", "Анатолий", "Немцов", "79182442818", "17 Bromwell Street", "46304" },
                    { 53, "г Новосибирск", "Elroy.Harvey@gmail.com", "Аблялиль", "Лапунова", "79145224042", "17 Bromwell Street", "46304" },
                    { 74, "г Новосибирск", "Barton_Labadie@yahoo.com", "Авагея", "Бурцев", "79211394433", "17 Bromwell Street", "30120" },
                    { 52, "г Новосибирск", "Brain.Gutkowski@hotmail.com", "Абдулька", "Ягубова", "79214604002", "17 Bromwell Street", "46304" },
                    { 75, "г Новосибирск", "Elroy.Harvey@gmail.com", "Абдурасуль", "Бархотов", "73832035454", "17 Bromwell Street", "30120" },
                    { 77, "г Новосибирск", "Christa.Block@hotmail.com", "Лев", "Дубинкин", "79263258303", "17 Bromwell Street", "11791" },
                    { 98, "г Новосибирск", "Annabel11@hotmail.com", "Всеволод", "Дюкова", "79175391429", "17 Bromwell Street", "48601" },
                    { 97, "г Новосибирск", "Colt_Kozey83@gmail.com", "Антон", "Берия", "79166113768", "17 Bromwell Street", "46304" },
                    { 96, "г Новосибирск", "Freddie62@gmail.com", "Аанжелла", "Ельцина", "79293409930", "17 Bromwell Street", "11791" },
                    { 95, "г Новосибирск", "Conner5@hotmail.com", "Римма", "Фамусов", "79222586517", "17 Bromwell Street", "48601" },
                    { 94, "г Новосибирск", "Daisy.Hansen@gmail.com", "Богдан", "Энтская", "79280809296", "17 Bromwell Street", "30120" },
                    { 93, "г Новосибирск", "Freddie62@gmail.com", "Арсен", "Жикина", "79105325941", "17 Bromwell Street", "30120" },
                    { 92, "г Новосибирск", "Colt_Kozey83@gmail.com", "Марсель", "Ялунина", "79194399119", "17 Bromwell Street", "11791" },
                    { 91, "г Новосибирск", "Demarco_Schulist39@hotmail.com", "Прохор", "Поливанов", "79139023581", "17 Bromwell Street", "48601" },
                    { 90, "г Новосибирск", "Cullen_Wiegand22@gmail.com", "Петр", "Казак", "79177725196", "17 Bromwell Street", "30120" },
                    { 89, "г Новосибирск", "Eldred_Stark10@gmail.com", "Руслан", "Давыдкина", "79209052714", "17 Bromwell Street", "48601" },
                    { 88, "г Новосибирск", "Camron.Goldner@hotmail.com", "Абраша", "Ельчуков", "79170829045", "17 Bromwell Street", "30120" },
                    { 87, "г Новосибирск", "Edyth24@yahoo.com", "Аблялиль", "Сыромятникова", "79151332992", "17 Bromwell Street", "46304" },
                    { 86, "г Новосибирск", "Etha.Hoppe79@gmail.com", "Степан", "Майструк", "79280809296", "17 Bromwell Street", "30120" },
                    { 85, "г Новосибирск", "Christa.Block@hotmail.com", "Матвей", "Коченков", "79132558821", "17 Bromwell Street", "48601" },
                    { 84, "г Новосибирск", "Eloise_Casper@hotmail.com", "Макар", "Авилова", "79240552734", "17 Bromwell Street", "11791" },
                    { 83, "г Новосибирск", "Daphnee.Huels30@gmail.com", "Роман", "Лукьяненко", "79175524506", "17 Bromwell Street", "48601" }
                });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Person",
                columns: new[] { "Id", "City", "Email", "FirstName", "LastName", "PhoneNumber", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 82, "г Новосибирск", "Elza_Gislason92@gmail.com", "Марк", "Энтская", "79274148070", "17 Bromwell Street", "48601" },
                    { 81, "г Новосибирск", "Cecile.Herman8@hotmail.com", "Абика", "Кондр", "79298189796", "17 Bromwell Street", "30120" },
                    { 80, "г Новосибирск", "Albina.Gleichner@hotmail.com", "Артемий", "Косыгина", "79118006386", "17 Bromwell Street", "48601" },
                    { 79, "г Новосибирск", "Annabel11@hotmail.com", "Владимир", "Козлитин", "79256060675", "17 Bromwell Street", "30120" },
                    { 78, "г Новосибирск", "Adam9@hotmail.com", "Клим", "Ямковой", "79228800561", "17 Bromwell Street", "48601" },
                    { 76, "г Новосибирск", "Corrine_Toy@hotmail.com", "Анатолий", "Кучеров", "79227798846", "17 Bromwell Street", "46304" },
                    { 51, "г Новосибирск", "Arvel28@yahoo.com", "Глеб", "Ельчуков", "79175391429", "17 Bromwell Street", "46304" },
                    { 50, "г Новосибирск", "Cassidy_Greenfelder10@hotmail.com", "Родион", "Аверина", "79249669496", "17 Bromwell Street", "11791" },
                    { 49, "г Новосибирск", "Ezekiel.Kilback@gmail.com", "Георгий", "Зуева", "79251390755", "17 Bromwell Street", "30120" },
                    { 22, "г Новосибирск", "Bonnie_Bode83@yahoo.com", "Платон", "Кравчиков", "79256060675", "17 Bromwell Street", "11791" },
                    { 21, "г Новосибирск", "Daniella22@yahoo.com", "Василий", "Зуева", "79170829045", "17 Bromwell Street", "48601" },
                    { 20, "г Новосибирск", "Claudia58@gmail.com", "Камиль", "Богоявленская", "79150056116", "17 Bromwell Street", "30120" },
                    { 19, "г Новосибирск", "Brandon.Schimmel87@gmail.com", "Нона", "Железнов", "79206112248", "17 Bromwell Street", "11791" },
                    { 18, "г Новосибирск", "Deron71@hotmail.com", "Константин", "Жикина", "79135453084", "17 Bromwell Street", "11791" },
                    { 17, "г Новосибирск", "Fleta85@hotmail.com", "Абдупатта", "Авилова", "79214483093", "17 Bromwell Street", "11791" },
                    { 16, "г Новосибирск", "Emery.Homenick@yahoo.com", "Кирилл", "Лукьяненко", "79222998760", "17 Bromwell Street", "11791" },
                    { 15, "г Новосибирск", "Corrine_Toy@hotmail.com", "Эльвира", "Ермолаева", "79222998760", "17 Bromwell Street", "46304" },
                    { 14, "г Новосибирск", "Bethany62@gmail.com", "Абдулния", "Ермолаева", "79152254604", "17 Bromwell Street", "48601" },
                    { 13, "г Новосибирск", "Allie.Thompson24@gmail.com", "Авгиння", "Чунц", "79116488841", "17 Bromwell Street", "30120" },
                    { 12, "г Новосибирск", "Dakota34@hotmail.com", "Макар", "Данильцина", "79124496807", "17 Bromwell Street", "30120" },
                    { 11, "г Новосибирск", "Cecile.Herman8@hotmail.com", "Августа", "Сыромятникова", "79153536338", "17 Bromwell Street", "48601" },
                    { 10, "г Новосибирск", "Conner5@hotmail.com", "Авгалифена", "Ягубова", "79136963602", "17 Bromwell Street", "30120" },
                    { 9, "г Новосибирск", "Arvel28@yahoo.com", "Авашерфа", "Антимонова", "79194399119", "17 Bromwell Street", "46304" },
                    { 8, "г Новосибирск", "Anabelle_OConnell@hotmail.com", "Абдулька", "Жарыхин", "79122253313", "17 Bromwell Street", "48601" },
                    { 7, "г Новосибирск", "Christa.Block@hotmail.com", "Давид", "Койтасбаева", "79301262016", "17 Bromwell Street", "11791" },
                    { 6, "г Новосибирск", "Anna24@hotmail.com", "Назар", "Булыгина", "79197853566", "17 Bromwell Street", "46304" },
                    { 5, "г Новосибирск", "Emery_Kreiger@gmail.com", "Дамир", "Рыжова", "79152254604", "17 Bromwell Street", "48601" },
                    { 4, "г Новосибирск", "Anderson_Huels@gmail.com", "Григорий", "Сыромятникова", "79206398166", "17 Bromwell Street", "11791" },
                    { 3, "г Новосибирск", "Conner5@hotmail.com", "Даниил", "Крылов", "79106585034", "17 Bromwell Street", "11791" },
                    { 2, "г Новосибирск", "Elroy.Harvey@gmail.com", "Николай", "Зуева", "79256060675", "17 Bromwell Street", "30120" },
                    { 23, "г Новосибирск", "Ferne_Kuhlman@hotmail.com", "Игнат", "Жуков", "79118006386", "17 Bromwell Street", "11791" },
                    { 24, "г Новосибирск", "Fleta85@hotmail.com", "Абдурасуль", "Ермолаева", "79166113768", "17 Bromwell Street", "11791" },
                    { 25, "г Новосибирск", "Clarabelle66@gmail.com", "Абдюль", "Богоявленская", "79116488841", "17 Bromwell Street", "11791" },
                    { 26, "г Новосибирск", "Derrick.Koelpin@yahoo.com", "Олег", "Ельчуков", "79147692876", "17 Bromwell Street", "11791" },
                    { 48, "г Новосибирск", "Celestino96@gmail.com", "Аващарфа", "Яковец", "79166113768", "17 Bromwell Street", "48601" },
                    { 47, "г Новосибирск", "Alexandra.Hudson95@yahoo.com", "Алан", "Вирановская", "79112864703", "17 Bromwell Street", "11791" },
                    { 46, "г Новосибирск", "Ezekiel.Kilback@gmail.com", "Абульфаза", "Вирановская", "79276075612", "17 Bromwell Street", "11791" },
                    { 45, "г Новосибирск", "Adriel87@hotmail.com", "Рамиль", "Бурцев", "79170829045", "17 Bromwell Street", "30120" },
                    { 44, "г Новосибирск", "Eve.Lind3@gmail.com", "Абрагина", "Кучеров", "79226596685", "17 Bromwell Street", "11791" },
                    { 43, "г Новосибирск", "Esther.Anderson@hotmail.com", "Ильяс", "Устимовича", "79123838261", "17 Bromwell Street", "48601" },
                    { 42, "г Новосибирск", "Amya_Dicki@hotmail.com", "Абель", "Поливанов", "79240552734", "17 Bromwell Street", "30120" },
                    { 41, "г Новосибирск", "Christian_Kirlin47@yahoo.com", "Диана", "Ягубова", "79214604002", "17 Bromwell Street", "30120" }
                });

            migrationBuilder.InsertData(
                schema: "Person",
                table: "Person",
                columns: new[] { "Id", "City", "Email", "FirstName", "LastName", "PhoneNumber", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { 40, "г Новосибирск", "Christa.Block@hotmail.com", "Дмитрий", "Энтская", "79170829045", "17 Bromwell Street", "11791" },
                    { 39, "г Новосибирск", "Frankie.Sporer@yahoo.com", "Рената", "Рязанов", "79181521761", "17 Bromwell Street", "11791" },
                    { 99, "г Новосибирск", "Evert_Schaefer@gmail.com", "Василий", "Ягубова", "79139023581", "17 Bromwell Street", "48601" },
                    { 38, "г Новосибирск", "Beryl.Thiel71@gmail.com", "Степан", "Сыромятникова", "79235970418", "17 Bromwell Street", "11791" },
                    { 36, "г Новосибирск", "Cullen_Wiegand22@gmail.com", "Мирослав", "Лебедев", "79236880618", "17 Bromwell Street", "48601" },
                    { 35, "г Новосибирск", "Frederik.Hintz50@gmail.com", "Аза", "Вирановская", "79263258303", "17 Bromwell Street", "46304" },
                    { 34, "г Новосибирск", "Conner5@hotmail.com", "Рената", "Железнов", "79175524506", "17 Bromwell Street", "46304" },
                    { 33, "г Новосибирск", "Alphonso.Dibbert@gmail.com", "Рамиль", "Лебедев", "79240552734", "17 Bromwell Street", "46304" },
                    { 32, "г Новосибирск", "Albina.Gleichner@hotmail.com", "Авгиння", "Чунц", "79175524506", "17 Bromwell Street", "30120" },
                    { 31, "г Новосибирск", "Brandon.Schimmel87@gmail.com", "Алексей", "Вирановская", "79251390755", "17 Bromwell Street", "46304" },
                    { 30, "г Новосибирск", "Colin.Ferry@yahoo.com", "Авашарфа", "Устимовича", "79139023581", "17 Bromwell Street", "46304" },
                    { 29, "г Новосибирск", "Anderson_Huels@gmail.com", "Ева", "Бархотов", "79274932037", "17 Bromwell Street", "11791" },
                    { 28, "г Новосибирск", "Cathryn_Halvorson82@hotmail.com", "Савелий", "Бурцева", "79249669496", "17 Bromwell Street", "11791" },
                    { 27, "г Новосибирск", "Bonnie_Bode83@yahoo.com", "Абам", "Коченков", "79185574147", "17 Bromwell Street", "46304" },
                    { 37, "г Новосибирск", "Ferne_Kuhlman@hotmail.com", "Михаил", "Лебедев", "79206398166", "17 Bromwell Street", "48601" },
                    { 100, "г Новосибирск", "Dayna38@gmail.com", "Валентин", "Ямковой", "79261414348", "17 Bromwell Street", "48601" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person",
                schema: "Person");
        }
    }
}
