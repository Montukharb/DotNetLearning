using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmptyProjectTesting.Migrations
{
    /// <inheritdoc />
    public partial class student_country_Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryFlag",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFlag", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HOD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_CountryFlag_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "CountryFlag",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CountryFlag",
                columns: new[] { "Code", "CapitalName", "CountryName", "CurrencyName" },
                values: new object[,]
                {
                    { "AD", "Andorra la Vella", "Andorra", "Euro" },
                    { "AE", "Abu Dhabi", "United Arab Emirates", "UAE Dirham" },
                    { "AF", "Kabul", "Afghanistan", "Afghani" },
                    { "AG", "Saint John's", "Antigua and Barbuda", "East Caribbean Dollar" },
                    { "AL", "Tirana", "Albania", "Lek" },
                    { "AM", "Yerevan", "Armenia", "Armenian Dram" },
                    { "AO", "Luanda", "Angola", "Kwanza" },
                    { "AR", "Buenos Aires", "Argentina", "Argentine Peso" },
                    { "AT", "Vienna", "Austria", "Euro" },
                    { "AU", "Canberra", "Australia", "Australian Dollar" },
                    { "AZ", "Baku", "Azerbaijan", "Azerbaijani Manat" },
                    { "BA", "Sarajevo", "Bosnia and Herzegovina", "Convertible Mark" },
                    { "BB", "Bridgetown", "Barbados", "Barbadian Dollar" },
                    { "BD", "Dhaka", "Bangladesh", "Taka" },
                    { "BE", "Brussels", "Belgium", "Euro" },
                    { "BF", "Ouagadougou", "Burkina Faso", "West African CFA Franc" },
                    { "BG", "Sofia", "Bulgaria", "Bulgarian Lev" },
                    { "BH", "Manama", "Bahrain", "Bahraini Dinar" },
                    { "BI", "Gitega", "Burundi", "Burundian Franc" },
                    { "BJ", "Porto-Novo", "Benin", "West African CFA Franc" },
                    { "BN", "Bandar Seri Begawan", "Brunei", "Brunei Dollar" },
                    { "BO", "Sucre", "Bolivia", "Boliviano" },
                    { "BR", "Brasilia", "Brazil", "Brazilian Real" },
                    { "BS", "Nassau", "Bahamas", "Bahamian Dollar" },
                    { "BT", "Thimphu", "Bhutan", "Ngultrum" },
                    { "BW", "Gaborone", "Botswana", "Pula" },
                    { "BY", "Minsk", "Belarus", "Belarusian Ruble" },
                    { "BZ", "Belmopan", "Belize", "Belize Dollar" },
                    { "CA", "Ottawa", "Canada", "Canadian Dollar" },
                    { "CD", "Kinshasa", "Democratic Republic of the Congo", "Congolese    Franc" },
                    { "CF", "Bangui", "Central African Republic", "Central African CFA    Franc" },
                    { "CG", "Brazzaville", "Republic of the Congo", "Central African CFA  Franc" },
                    { "CH", "Bern", "Switzerland", "Swiss Franc" },
                    { "CI", "Yamoussoukro", "Cote d'Ivoire", "West African CFA Franc" },
                    { "CL", "Santiago", "Chile", "Chilean Peso" },
                    { "CM", "Yaounde", "Cameroon", "Central African CFA Franc" },
                    { "CN", "Beijing", "China", "Chinese Yuan" },
                    { "CO", "Bogota", "Colombia", "Colombian Peso" },
                    { "CR", "San Jose", "Costa Rica", "Costa Rican Colon" },
                    { "CU", "Havana", "Cuba", "Cuban Peso" },
                    { "CV", "Praia", "Cabo Verde", "Cape Verdean Escudo" },
                    { "CY", "Nicosia", "Cyprus", "Euro" },
                    { "CZ", "Prague", "Czech Republic", "Czech Koruna" },
                    { "DE", "Berlin", "Germany", "Euro" },
                    { "DJ", "Djibouti", "Djibouti", "Djiboutian Franc" },
                    { "DK", "Copenhagen", "Denmark", "Danish Krone" },
                    { "DM", "Roseau", "Dominica", "East Caribbean Dollar" },
                    { "DO", "Santo Domingo", "Dominican Republic", "Dominican Peso" },
                    { "DZ", "Algiers", "Algeria", "Algerian Dinar" },
                    { "EC", "Quito", "Ecuador", "US Dollar" },
                    { "EE", "Tallinn", "Estonia", "Euro" },
                    { "EG", "Cairo", "Egypt", "Egyptian Pound" },
                    { "ER", "Asmara", "Eritrea", "Nakfa" },
                    { "ES", "Madrid", "Spain", "Euro" },
                    { "ET", "Addis Ababa", "Ethiopia", "Birr" },
                    { "FI", "Helsinki", "Finland", "Euro" },
                    { "FJ", "Suva", "Fiji", "Fijian Dollar" },
                    { "FM", "Palikir", "Micronesia", "US Dollar" },
                    { "FR", "Paris", "France", "Euro" },
                    { "GA", "Libreville", "Gabon", "Central African CFA Franc" },
                    { "GB", "London", "United Kingdom", "Pound Sterling" },
                    { "GD", "St. George's", "Grenada", "East Caribbean Dollar" },
                    { "GE", "Tbilisi", "Georgia", "Georgian Lari" },
                    { "GH", "Accra", "Ghana", "Ghanaian Cedi" },
                    { "GM", "Banjul", "Gambia", "Dalasi" },
                    { "GN", "Conakry", "Guinea", "Guinean Franc" },
                    { "GQ", "Malabo", "Equatorial Guinea", "Central African CFA Franc" },
                    { "GR", "Athens", "Greece", "Euro" },
                    { "GT", "Guatemala City", "Guatemala", "Quetzal" },
                    { "GW", "Bissau", "Guinea-Bissau", "West African CFA Franc" },
                    { "GY", "Georgetown", "Guyana", "Guyanese Dollar" },
                    { "HN", "Tegucigalpa", "Honduras", "Lempira" },
                    { "HR", "Zagreb", "Croatia", "Euro" },
                    { "HT", "Port-au-Prince", "Haiti", "Gourde" },
                    { "HU", "Budapest", "Hungary", "Forint" },
                    { "ID", "Jakarta", "Indonesia", "Rupiah" },
                    { "IE", "Dublin", "Ireland", "Euro" },
                    { "IL", "Jerusalem", "Israel", "Israeli New Shekel" },
                    { "IN", "New Delhi", "India", "Indian Rupee" },
                    { "IQ", "Baghdad", "Iraq", "Iraqi Dinar" },
                    { "IR", "Tehran", "Iran", "Iranian Rial" },
                    { "IS", "Reykjavik", "Iceland", "Icelandic Krona" },
                    { "IT", "Rome", "Italy", "Euro" },
                    { "JM", "Kingston", "Jamaica", "Jamaican Dollar" },
                    { "JO", "Amman", "Jordan", "Jordanian Dinar" },
                    { "JP", "Tokyo", "Japan", "Japanese Yen" },
                    { "KE", "Nairobi", "Kenya", "Kenyan Shilling" },
                    { "KG", "Bishkek", "Kyrgyzstan", "Som" },
                    { "KH", "Phnom Penh", "Cambodia", "Riel" },
                    { "KI", "South Tarawa", "Kiribati", "Australian Dollar" },
                    { "KM", "Moroni", "Comoros", "Comorian Franc" },
                    { "KN", "Basseterre", "Saint Kitts and Nevis", "East Caribbean Dollar" },
                    { "KP", "Pyongyang", "North Korea", "North Korean Won" },
                    { "KR", "Seoul", "South Korea", "South Korean Won" },
                    { "KW", "Kuwait City", "Kuwait", "Kuwaiti Dinar" },
                    { "KZ", "Astana", "Kazakhstan", "Tenge" },
                    { "LA", "Vientiane", "Laos", "Kip" },
                    { "LB", "Beirut", "Lebanon", "Lebanese Pound" },
                    { "LC", "Castries", "Saint Lucia", "East Caribbean Dollar" },
                    { "LI", "Vaduz", "Liechtenstein", "Swiss Franc" },
                    { "LK", "Sri Jayawardenepura Kotte", "Sri Lanka", "Sri Lankan Rupee" },
                    { "LR", "Monrovia", "Liberia", "Liberian Dollar" },
                    { "LS", "Maseru", "Lesotho", "Loti" },
                    { "LT", "Vilnius", "Lithuania", "Euro" },
                    { "LU", "Luxembourg City", "Luxembourg", "Euro" },
                    { "LV", "Riga", "Latvia", "Euro" },
                    { "LY", "Tripoli", "Libya", "Libyan Dinar" },
                    { "MA", "Rabat", "Morocco", "Moroccan Dirham" },
                    { "MC", "Monaco", "Monaco", "Euro" },
                    { "MD", "Chisinau", "Moldova", "Moldovan Leu" },
                    { "ME", "Podgorica", "Montenegro", "Euro" },
                    { "MG", "Antananarivo", "Madagascar", "Ariary" },
                    { "MH", "Majuro", "Marshall Islands", "US Dollar" },
                    { "MK", "Skopje", "North Macedonia", "Denar" },
                    { "ML", "Bamako", "Mali", "West African CFA Franc" },
                    { "MM", "Naypyidaw", "Myanmar", "Kyat" },
                    { "MN", "Ulaanbaatar", "Mongolia", "Tugrik" },
                    { "MR", "Nouakchott", "Mauritania", "Ouguiya" },
                    { "MT", "Valletta", "Malta", "Euro" },
                    { "MU", "Port Louis", "Mauritius", "Mauritian Rupee" },
                    { "MV", "Male", "Maldives", "Maldivian Rufiyaa" },
                    { "MW", "Lilongwe", "Malawi", "Malawian Kwacha" },
                    { "MX", "Mexico City", "Mexico", "Mexican Peso" },
                    { "MY", "Kuala Lumpur", "Malaysia", "Malaysian Ringgit" },
                    { "MZ", "Maputo", "Mozambique", "Metical" },
                    { "NA", "Windhoek", "Namibia", "Namibian Dollar" },
                    { "NE", "Niamey", "Niger", "West African CFA Franc" },
                    { "NG", "Abuja", "Nigeria", "Naira" },
                    { "NI", "Managua", "Nicaragua", "Cordoba" },
                    { "NL", "Amsterdam", "Netherlands", "Euro" },
                    { "NO", "Oslo", "Norway", "Norwegian Krone" },
                    { "NP", "Kathmandu", "Nepal", "Nepalese Rupee" },
                    { "NR", "Yaren", "Nauru", "Australian Dollar" },
                    { "NZ", "Wellington", "New Zealand", "New Zealand Dollar" },
                    { "OM", "Muscat", "Oman", "Omani Rial" },
                    { "PA", "Panama City", "Panama", "Balboa" },
                    { "PE", "Lima", "Peru", "Sol" },
                    { "PG", "Port Moresby", "Papua New Guinea", "Kina" },
                    { "PH", "Manila", "Philippines", "Philippine Peso" },
                    { "PK", "Islamabad", "Pakistan", "Pakistani Rupee" },
                    { "PL", "Warsaw", "Poland", "Zloty" },
                    { "PT", "Lisbon", "Portugal", "Euro" },
                    { "PW", "Ngerulmud", "Palau", "US Dollar" },
                    { "PY", "Asuncion", "Paraguay", "Guarani" },
                    { "QA", "Doha", "Qatar", "Qatari Riyal" },
                    { "RO", "Bucharest", "Romania", "Romanian Leu" },
                    { "RS", "Belgrade", "Serbia", "Serbian Dinar" },
                    { "RU", "Moscow", "Russia", "Russian Ruble" },
                    { "RW", "Kigali", "Rwanda", "Rwandan Franc" },
                    { "SA", "Riyadh", "Saudi Arabia", "Saudi Riyal" },
                    { "SB", "Honiara", "Solomon Islands", "Solomon Islands Dollar" },
                    { "SC", "Victoria", "Seychelles", "Seychellois Rupee" },
                    { "SD", "Khartoum", "Sudan", "Sudanese Pound" },
                    { "SE", "Stockholm", "Sweden", "Swedish Krona" },
                    { "SG", "Singapore", "Singapore", "Singapore Dollar" },
                    { "SI", "Ljubljana", "Slovenia", "Euro" },
                    { "SK", "Bratislava", "Slovakia", "Euro" },
                    { "SL", "Freetown", "Sierra Leone", "Leone" },
                    { "SM", "San Marino", "San Marino", "Euro" },
                    { "SN", "Dakar", "Senegal", "West African CFA Franc" },
                    { "SO", "Mogadishu", "Somalia", "Somali Shilling" },
                    { "SR", "Paramaribo", "Suriname", "Surinamese Dollar" },
                    { "SS", "Juba", "South Sudan", "South Sudanese Pound" },
                    { "ST", "Sao Tome", "Sao Tome and Principe", "Dobra" },
                    { "SV", "San Salvador", "El Salvador", "US Dollar" },
                    { "SY", "Damascus", "Syria", "Syrian Pound" },
                    { "SZ", "Mbabane", "Eswatini", "Lilangeni" },
                    { "TD", "N'Djamena", "Chad", "Central African CFA Franc" },
                    { "TG", "Lome", "Togo", "West African CFA Franc" },
                    { "TH", "Bangkok", "Thailand", "Baht" },
                    { "TJ", "Dushanbe", "Tajikistan", "Somoni" },
                    { "TL", "Dili", "Timor-Leste", "US Dollar" },
                    { "TM", "Ashgabat", "Turkmenistan", "Turkmen Manat" },
                    { "TN", "Tunis", "Tunisia", "Tunisian Dinar" },
                    { "TO", "Nuku'alofa", "Tonga", "Pa'anga" },
                    { "TR", "Ankara", "Turkey", "Turkish Lira" },
                    { "TT", "Port of Spain", "Trinidad and Tobago", "Trinidad and Tobago Dollar" },
                    { "TV", "Funafuti", "Tuvalu", "Australian Dollar" },
                    { "TW", "Taipei", "Taiwan", "New Taiwan Dollar" },
                    { "TZ", "Dodoma", "Tanzania", "Tanzanian Shilling" },
                    { "UA", "Kyiv", "Ukraine", "Hryvnia" },
                    { "UG", "Kampala", "Uganda", "Ugandan Shilling" },
                    { "US", "Washington, D.C.", "United States", "US Dollar" },
                    { "UY", "Montevideo", "Uruguay", "Uruguayan Peso" },
                    { "UZ", "Tashkent", "Uzbekistan", "Uzbekistani Som" },
                    { "VA", "Vatican City", "Vatican City", "Euro" },
                    { "VC", "Kingstown", "Saint Vincent and the Grenadines", "East Caribbean Dollar" },
                    { "VE", "Caracas", "Venezuela", "Bolivar" },
                    { "VN", "Hanoi", "Vietnam", "Dong" },
                    { "VU", "Port Vila", "Vanuatu", "Vatu" },
                    { "WS", "Apia", "Samoa", "Tala" },
                    { "YE", "Sana'a", "Yemen", "Yemeni Rial" },
                    { "ZA", "Pretoria", "South Africa", "South African Rand" },
                    { "ZM", "Lusaka", "Zambia", "Zambian Kwacha" },
                    { "ZW", "Harare", "Zimbabwe", "Zimbabwe Gold" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "HOD", "Name" },
                values: new object[,]
                {
                    { 1, "Dr. Rajesh Sharma", "Computer Science Department" },
                    { 2, "Dr. Priya Verma", "Information Technology Department" },
                    { 3, "Dr. Amit Gupta", "Electronics Engineering Department" },
                    { 4, "Dr. Vikram Singh", "Mechanical Engineering Department" },
                    { 5, "Dr. Neha Bansal", "Civil Engineering Department" },
                    { 6, "Dr. Anil Kumar", "Electrical Engineering Department" },
                    { 7, "Dr. Sunita Mehta", "Mathematics Department" },
                    { 8, "Dr. Rakesh Malhotra", "Physics Department" },
                    { 9, "Dr. Pooja Arora", "Chemistry Department" },
                    { 10, "Dr. Rabindranath Tagore", "Biology Department" },
                    { 11, "Dr. Deepak Aggarwal", "Commerce Department" },
                    { 12, "Dr. Kavita Yadav", "Management Studies Department" },
                    { 13, "Dr. Nitin Sethi", "Economics Department" },
                    { 14, "Dr. Shalini Kapoor", "English Department" },
                    { 15, "Dr. Suresh Chandra", "Hindi Department" },
                    { 16, "Dr. Ashok Mishra", "History Department" },
                    { 17, "Dr. Meenakshi Jain", "Political Science Department" },
                    { 18, "Dr. Rohit Khanna", "Psychology Department" },
                    { 19, "Dr. Garima Joshi", "Environmental Science Department" },
                    { 20, "Dr. Arjun Malhotra", "Data Science Department" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CountryCode",
                table: "Students",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CountryFlag");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
