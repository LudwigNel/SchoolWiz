using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolWiz.Persistence.Migrations
{
    public partial class Updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("74ed2c1b-bb5d-4d4b-ba4a-736623948338"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a80cb639-3326-4bad-a898-3d38abe8aca6"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new Guid("7de61f4a-cae0-4b22-b661-ad7ce4ba6a68") });

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("001b5ad8-a911-44f9-8368-db87ee7e53fc"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("15b8eac7-e3ba-4e07-a046-268e4562dfd6"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1ba9da78-2ff7-4f85-8bf2-aae2ebd45c9d"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("28960afc-caa9-4900-9b7b-0a048b918553"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("2b87a894-b7c0-4fe9-9e07-6f5ef9361aca"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4bed07fa-e936-410d-afdc-bbc60bc9d67b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4c319d10-1bc8-4357-9f1e-e6a8cc77a91c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4ee8cf0f-3920-4d55-b6df-251e150bd399"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("5c2571be-ef1c-4675-917e-6acfe1d38818"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("69b02534-4c59-40ac-b0a9-08d96259ce6d"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6cd02125-8b73-46eb-9ecb-ca1deaf213f1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("715a7ad4-1752-42d5-bce7-d34d0b71aa8a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("722abd8b-0245-4e86-ac75-3001914a4c0f"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7893f447-c8ab-4510-82ee-83541ceaa063"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("83d9d0dd-d1c1-4176-8523-d7da9f11b2a4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8b1cb13d-e815-4117-a7e9-0e74293ca7d2"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8f32da1a-7084-4689-a0d5-9a2fea8894c0"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a2569574-531e-42f1-8d6f-5ffc17a0b111"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("a610fad8-9cba-4889-ade0-5a78c8b9ccdd"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b5b3d54c-78c8-4834-b002-68dccc7eca72"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ba851696-0c7e-4f2f-95a6-42b8c577f50a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c0407c47-e3c1-4186-ac02-a713313d9746"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c204e515-c218-4241-ad7f-83839b816e82"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c4cf1032-3067-4a36-948b-b282119542dc"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c572687c-0c05-43eb-a124-d57007daed76"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("da5304c0-3b47-43a5-a491-6998df095e2e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("da969129-bd50-43fb-9cf1-94e444ad2b30"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("dc919018-beca-4fd6-b81c-2ee3b05fdd48"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("dd8b3ac3-fa38-4a3d-aee1-3ac73bb2ee52"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("df0a6b64-fdb2-4857-8da6-6be643b23b9c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("eca79886-9f2e-4123-b7e6-7154cd453a81"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f506e027-5c52-4822-8f86-0c0281e8962a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f9afae8a-5a69-4399-bb56-772fe92f096e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fdc3375b-89df-447e-a794-b65aae1ec8f4"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("00846b58-fa6b-4e37-8167-5551fb98a50e"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("12b627df-7398-47be-85d0-696f3998cd12"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("1c092bfb-41de-40c1-8598-407e29b532f1"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("56e9ac38-5afa-4420-bfe9-5089585c2f8a"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("65533289-5ff8-4c02-9f30-6280d4286095"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("7a73463e-2623-4111-a7e3-c6f8e1261736"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("8f310324-4cf2-4df6-b2c3-f1ae3528a78e"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("9f3a1f81-6e4d-46b3-b014-54c52ecfdf10"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("b2234fe3-c99e-4e2c-9330-e264b281c48c"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("c5eeb2e7-a5c4-489e-a42d-7b642b639102"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("dc1f4bf1-0398-4857-82e6-1a24d3ff2046"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("e96d6026-e422-45a4-bd44-650b4af3f59a"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("05fbcee1-dbb8-478c-9120-a79d5a2b48e2"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("3265a53a-e153-4145-ae18-b7bf1c18a1b7"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("3731fb9c-1af8-49db-a3a0-490e87457352"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("3f7af9e3-123b-420e-8d5a-682ef4447359"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("4f7ac751-ba4e-413f-a6de-cb0e1c6c38dc"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("538ef536-482e-47c8-aefd-81edd66712fd"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("6f242042-db42-42ee-a6b7-1f2632d69fd2"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("ba2191fd-c65a-45a2-953a-16c54345a9e1"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: new Guid("870d64c4-da18-41c3-8bcb-9c7c5d8b13e4"));

            migrationBuilder.DeleteData(
                table: "Vat",
                keyColumn: "Id",
                keyValue: new Guid("f42a336b-2aad-4a5b-8315-630134560876"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7de61f4a-cae0-4b22-b661-ad7ce4ba6a68"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("171095d1-fc7c-4348-a834-b801621fbcad"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"));

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: new Guid("0943a261-05b3-4334-9090-7b053166c2ce"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(4259) });

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: new Guid("4ab8918d-44c4-48f0-a7ac-11e1b21739bc"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(4287) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedByDate", "ModifiedById" },
                values: new object[,]
                {
                    { new Guid("4d72ca6b-6a9f-4f2f-b816-fe96dc08d1f2"), "f8b710e0-56dc-4717-b8d5-845836710e5c", "ApplicationRole", "Administrator", "Administrator", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null },
                    { new Guid("fcc4f5ce-7bf7-405f-906e-68c839a12439"), "fa1cfacb-21b0-477c-8f27-b53e3f5f8158", "ApplicationRole", "Billing", "Billing", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null },
                    { new Guid("7d8f90f5-761b-4136-864a-8ddec2a4a754"), "03a40b3a-c2a8-427f-8b0e-8a206743c1bc", "ApplicationRole", "Teacher", "Teacher", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedById", "DateCreated", "Email", "EmailConfirmed", "FirstName", "IdentityNumber", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "MobileNumber", "ModifiedById", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), 0, "cec7ca53-24d5-4dce-ae16-15d43a1c1633", new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 12, 29, 21, 516, DateTimeKind.Local).AddTicks(7806), ";udwign@lucasdev.co.za", true, "Ludwig", "8101145019087", false, "Nel", false, null, "Paul", "0835651243", null, null, "LUDWIGN@LUCASDEV.CO.ZA", "LUDWIGN@LUCASDEV.CO.ZA", "AQAAAAEAACcQAAAAEHF9Gu6chaM9xAyxb9YRSNVLdTW0yykw93sVTInHlOtnqoY6fUMNMG3qybuKyBhZ9g==", null, false, "00000000-0000-0000-0000-000000000000", false, "ludwign@lucasdev.co.za" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[] { new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(7059), false, null, null, "South Africa" });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("5941a142-8280-4e6f-9c30-854b7d129e4f"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6248), false, null, null, "12" },
                    { new Guid("8098792a-0267-4d94-81ac-8ab24c5bda90"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6245), false, null, null, "10" },
                    { new Guid("d2312d25-3d2b-49ec-8ff2-dfb1fee357b6"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6243), false, null, null, "9" },
                    { new Guid("369f5897-4bc4-4899-9772-4a51398f576f"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6242), false, null, null, "8" },
                    { new Guid("7e625f5d-0256-4814-8af1-abe70a56f704"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6246), false, null, null, "11" },
                    { new Guid("b1c259c6-8995-4f96-9060-e1c9cb0e6c60"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6229), false, null, null, "6" },
                    { new Guid("9a892619-e36c-418e-be00-fd5858e9b352"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6228), false, null, null, "5" },
                    { new Guid("09b463be-8d65-401c-bdc4-704598503960"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6225), false, null, null, "4" },
                    { new Guid("472f8e10-348f-42a1-99c9-cf0b31e2e836"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6223), false, null, null, "3" },
                    { new Guid("a335c72b-e31d-46e2-b398-4dea15cb2c85"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6221), false, null, null, "2" },
                    { new Guid("f2bebaed-4e94-4712-a0b2-cfce5b89451b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6192), false, null, null, "1" },
                    { new Guid("ecd28d1d-90f6-4507-9a78-3c41a0b5ab3c"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(6231), false, null, null, "7" }
                });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("1a227c52-a576-4222-8afc-424921239072"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5375) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("3087b823-1620-4e7b-87fd-b5dd45f3d5f3"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5379) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("73d81136-2647-45c7-a011-018f6bed2c8a"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5377) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("8296c7a7-62e5-4ffa-bcda-8eef49dbb433"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5332) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("b24a381b-d887-4af2-a3db-cd1226e03e86"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5369) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("b9946f91-8e24-492f-9656-33fe2feeebc2"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(5372) });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "ContactPerson", "CreatedById", "CreatedDate", "Email", "ImageUrl", "IsDeleted", "ModifiedById", "ModifiedDate", "Name", "PhoneNumber", "RegistrationNo", "VatNo" },
                values: new object[] { new Guid("3dac1439-090a-4e1d-b01f-a0afd4e83091"), "Ted Coffin", new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 12, 29, 21, 533, DateTimeKind.Local).AddTicks(9274), "tedc@destinychangeracademy.co.za", null, false, null, null, "Destiny Changers Academy", "0824693520", "", "" });

            migrationBuilder.InsertData(
                table: "Vat",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ValidFrom", "ValidTo", "Value" },
                values: new object[] { new Guid("37f9006b-6ce3-4052-a2df-0d8ed0848c25"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 12, 29, 21, 535, DateTimeKind.Local).AddTicks(1224), false, null, null, null, null, 15m });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountStatuses",
                keyColumn: "Id",
                keyValue: new Guid("55b35573-7d5c-4142-8c7f-9e1ce1425898"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(705) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountStatuses",
                keyColumn: "Id",
                keyValue: new Guid("de4649fd-e49d-43f7-9e04-a779cfade4ae"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(739) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("068cd348-f51f-4d5e-b265-5ff8db1de74b"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(3027) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("23ec936b-c364-4e7b-bffd-52dffa0b4c94"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(3023) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("9ceda47f-c8ba-4af3-a9e0-dbf4adbe80b1"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(2985) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new Guid("4d72ca6b-6a9f-4f2f-b816-fe96dc08d1f2") });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("403bcfb4-15d9-4833-92b1-21641d7be853"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8220), false, null, null, "Eastern Cape" },
                    { new Guid("90ee2c5f-57b7-48e2-a25c-b2a126d5b9d3"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8255), false, null, null, "Free state" },
                    { new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8257), false, null, null, "Gauteng" },
                    { new Guid("bdf8a156-274b-4034-85e2-2b9ffcf77d2e"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8259), false, null, null, "KwaZulu-Natal" },
                    { new Guid("bc05dda5-93a8-4318-922b-958164727e38"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8260), false, null, null, "Limpopo" },
                    { new Guid("12ee319f-6e8a-43ad-a3d2-68eb125aba01"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8263), false, null, null, "Mpumalanga" },
                    { new Guid("87568b2a-e5cd-421d-beb6-d82eefaa6a98"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8264), false, null, null, "North West" },
                    { new Guid("3ec29e86-1983-46b9-a611-bab4a9dd7bcb"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8266), false, null, null, "Northern Cape" },
                    { new Guid("67a1a6ec-8831-42dd-8b6a-17ce0b2ca471"), new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(8267), false, null, null, "Western Cape" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { new Guid("400b1f6a-da3d-4964-96bb-2e56e36dcef4"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9505), false, null, null, "Alexandra", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("f3cfe5d6-1cdb-416e-b525-ac2ceacdad9e"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9593), false, null, null, "Randfontein", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("e60e5b7a-e62c-4843-b90c-003759abad63"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9592), false, null, null, "Magaliesburg", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("88397a66-0eaf-4108-8309-a1a97ecf99c0"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9590), false, null, null, "Krugersdorp", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("042d44f0-e0e9-4e6f-be2a-d2168b09c524"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9589), false, null, null, "Heidelberg", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("9128bdb7-ed22-4d3f-916f-d3d36fb3b93b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9587), false, null, null, "Meyerton", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("888733bb-1e12-41d2-ba1d-10bf449009a8"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9585), false, null, null, "Vereeniging", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("bb12096c-813c-49d4-9110-b0e55bf63b77"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9584), false, null, null, "Vanderbijlpark", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("4f193e1a-10ad-4098-830d-84c13da89b41"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9581), false, null, null, "Pretoria", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("c63999e8-0024-4cf6-a2c4-5597fdb6565a"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9579), false, null, null, "Irene", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("462b558e-eabc-4db9-b284-f311614b8c64"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9577), false, null, null, "Hammanskraal", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("c903cd6e-696f-408e-880b-ba9b5a585cb9"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9576), false, null, null, "Centurion", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("c10d5f2c-891c-4871-adc5-c11b70e39e2a"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9574), false, null, null, "Tembisa", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("0839ee73-aa0b-40d4-86a9-6c4e38704d45"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9573), false, null, null, "Springs", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("e98751a8-65bb-4c9c-87ea-ffc19501f8d7"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9571), false, null, null, "Reiger Park", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("dfcc28c4-ccd3-447a-8f5e-fdf6553c20d0"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9570), false, null, null, "Nigel", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("6311dda9-6e96-4021-a7f8-7a34f49552c2"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9567), false, null, null, "Kempton Park", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("f8217604-1677-4dd8-8efb-6d39de07f3ca"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9565), false, null, null, "Isando", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("3e080284-726a-4ad9-ba92-7aad461162c4"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9540), false, null, null, "Johannesburg", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("ddaad733-161e-42f6-9376-2f383ad681e9"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9543), false, null, null, "Lenasia", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("d41d5548-a92b-4616-b561-f12fe750674b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9544), false, null, null, "Midrand", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("63b55693-85b9-4631-8ecf-79f29c0f6eaa"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9546), false, null, null, "Randburg", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("b4313ab9-7cf6-46e0-9c9b-a227e6d84ce5"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9548), false, null, null, "Roodepoort", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("834b3cca-d2b4-443c-9c54-bde52ad8000b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9549), false, null, null, "Sandton", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("8b9287e0-72f4-45d6-a34c-d7dc3ba60d9c"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9595), false, null, null, "Bekkersdal", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("fa635702-1d94-4be8-8feb-a5d9c3b8bdb9"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9551), false, null, null, "Soweto", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("1aa2d8e2-f85e-456c-b211-2900a2ac911b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9556), false, null, null, "Benoni", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("fecfaa5e-1c57-4806-b0e9-e62d6b0c621e"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9558), false, null, null, "Boksburg", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("d7c43955-ca7f-44ca-9caa-e71cdd3e79d3"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9559), false, null, null, "Brakpan", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("deda1e34-f19d-4263-963c-6f042892965e"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9561), false, null, null, "Daveyton", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("56e01e6f-939d-4785-9c40-1a9b293b36d3"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9562), false, null, null, "Edenvale", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("8b74b470-555b-486f-beb7-fbc16ea25b1b"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9564), false, null, null, "Germiston", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("1438f6de-7683-4a78-b6c7-705b04c42ab1"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9552), false, null, null, "Alberton", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") },
                    { new Guid("37c63acd-4214-4b0d-8fab-01ab8156ed8c"), new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new DateTime(2020, 11, 6, 10, 29, 21, 534, DateTimeKind.Utc).AddTicks(9598), false, null, null, "Westonaria", new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7d8f90f5-761b-4136-864a-8ddec2a4a754"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fcc4f5ce-7bf7-405f-906e-68c839a12439"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"), new Guid("4d72ca6b-6a9f-4f2f-b816-fe96dc08d1f2") });

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("042d44f0-e0e9-4e6f-be2a-d2168b09c524"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("0839ee73-aa0b-40d4-86a9-6c4e38704d45"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1438f6de-7683-4a78-b6c7-705b04c42ab1"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("1aa2d8e2-f85e-456c-b211-2900a2ac911b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("37c63acd-4214-4b0d-8fab-01ab8156ed8c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3e080284-726a-4ad9-ba92-7aad461162c4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("400b1f6a-da3d-4964-96bb-2e56e36dcef4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("462b558e-eabc-4db9-b284-f311614b8c64"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("4f193e1a-10ad-4098-830d-84c13da89b41"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("56e01e6f-939d-4785-9c40-1a9b293b36d3"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("6311dda9-6e96-4021-a7f8-7a34f49552c2"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("63b55693-85b9-4631-8ecf-79f29c0f6eaa"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("834b3cca-d2b4-443c-9c54-bde52ad8000b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("88397a66-0eaf-4108-8309-a1a97ecf99c0"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("888733bb-1e12-41d2-ba1d-10bf449009a8"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8b74b470-555b-486f-beb7-fbc16ea25b1b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("8b9287e0-72f4-45d6-a34c-d7dc3ba60d9c"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("9128bdb7-ed22-4d3f-916f-d3d36fb3b93b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("b4313ab9-7cf6-46e0-9c9b-a227e6d84ce5"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("bb12096c-813c-49d4-9110-b0e55bf63b77"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c10d5f2c-891c-4871-adc5-c11b70e39e2a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c63999e8-0024-4cf6-a2c4-5597fdb6565a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("c903cd6e-696f-408e-880b-ba9b5a585cb9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d41d5548-a92b-4616-b561-f12fe750674b"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d7c43955-ca7f-44ca-9caa-e71cdd3e79d3"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("ddaad733-161e-42f6-9376-2f383ad681e9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("deda1e34-f19d-4263-963c-6f042892965e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("dfcc28c4-ccd3-447a-8f5e-fdf6553c20d0"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e60e5b7a-e62c-4843-b90c-003759abad63"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("e98751a8-65bb-4c9c-87ea-ffc19501f8d7"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f3cfe5d6-1cdb-416e-b525-ac2ceacdad9e"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f8217604-1677-4dd8-8efb-6d39de07f3ca"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fa635702-1d94-4be8-8feb-a5d9c3b8bdb9"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("fecfaa5e-1c57-4806-b0e9-e62d6b0c621e"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("09b463be-8d65-401c-bdc4-704598503960"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("369f5897-4bc4-4899-9772-4a51398f576f"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("472f8e10-348f-42a1-99c9-cf0b31e2e836"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("5941a142-8280-4e6f-9c30-854b7d129e4f"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("7e625f5d-0256-4814-8af1-abe70a56f704"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("8098792a-0267-4d94-81ac-8ab24c5bda90"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("9a892619-e36c-418e-be00-fd5858e9b352"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("a335c72b-e31d-46e2-b398-4dea15cb2c85"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("b1c259c6-8995-4f96-9060-e1c9cb0e6c60"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("d2312d25-3d2b-49ec-8ff2-dfb1fee357b6"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("ecd28d1d-90f6-4507-9a78-3c41a0b5ab3c"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("f2bebaed-4e94-4712-a0b2-cfce5b89451b"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("12ee319f-6e8a-43ad-a3d2-68eb125aba01"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("3ec29e86-1983-46b9-a611-bab4a9dd7bcb"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("403bcfb4-15d9-4833-92b1-21641d7be853"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("67a1a6ec-8831-42dd-8b6a-17ce0b2ca471"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("87568b2a-e5cd-421d-beb6-d82eefaa6a98"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("90ee2c5f-57b7-48e2-a25c-b2a126d5b9d3"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("bc05dda5-93a8-4318-922b-958164727e38"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("bdf8a156-274b-4034-85e2-2b9ffcf77d2e"));

            migrationBuilder.DeleteData(
                table: "Schools",
                keyColumn: "Id",
                keyValue: new Guid("3dac1439-090a-4e1d-b01f-a0afd4e83091"));

            migrationBuilder.DeleteData(
                table: "Vat",
                keyColumn: "Id",
                keyValue: new Guid("37f9006b-6ce3-4052-a2df-0d8ed0848c25"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d72ca6b-6a9f-4f2f-b816-fe96dc08d1f2"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b98f1f13-302a-406a-83c1-faff38f72b77"));

            migrationBuilder.DeleteData(
                table: "Provinces",
                keyColumn: "Id",
                keyValue: new Guid("51c34b2c-9074-4f02-9fcc-c8c8652adb15"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("f502aa59-b2e0-4626-ae06-bb0820df807d"));

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: new Guid("0943a261-05b3-4334-9090-7b053166c2ce"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(4825) });

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: new Guid("4ab8918d-44c4-48f0-a7ac-11e1b21739bc"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(4853) });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedByDate", "ModifiedById" },
                values: new object[,]
                {
                    { new Guid("7de61f4a-cae0-4b22-b661-ad7ce4ba6a68"), "5939e8f8-cc7f-416f-bd7c-262a327780ca", "ApplicationRole", "Administrator", "Administrator", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null },
                    { new Guid("74ed2c1b-bb5d-4d4b-ba4a-736623948338"), "220e7e65-8b62-4073-a61a-b85b320302b9", "ApplicationRole", "Billing", "Billing", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null },
                    { new Guid("a80cb639-3326-4bad-a898-3d38abe8aca6"), "30152ae0-d568-43e3-b006-a77b42809076", "ApplicationRole", "Teacher", "Teacher", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedById", "DateCreated", "Email", "EmailConfirmed", "FirstName", "IdentityNumber", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "MiddleName", "MobileNumber", "ModifiedById", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), 0, "68ac08a0-9d4e-43ea-a21d-f4218200371a", new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 14, 26, 49, 785, DateTimeKind.Local).AddTicks(505), ";udwign@lucasdev.co.za", true, "Ludwig", "8101145019087", false, "Nel", false, null, "Paul", "0835651243", null, null, "LUDWIGN@LUCASDEV.CO.ZA", "LUDWIGN@LUCASDEV.CO.ZA", "AQAAAAEAACcQAAAAECPuXoKeqTJAqXDvLGfv7Jc51TodyGAbVTyV/KPFX/gKaEWr6VDACL6ILLsgzmSJwA==", null, false, "00000000-0000-0000-0000-000000000000", false, "ludwign@lucasdev.co.za" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[] { new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6855), false, null, null, "South Africa" });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("7a73463e-2623-4111-a7e3-c6f8e1261736"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6314), false, null, null, "12" },
                    { new Guid("9f3a1f81-6e4d-46b3-b014-54c52ecfdf10"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6311), false, null, null, "10" },
                    { new Guid("dc1f4bf1-0398-4857-82e6-1a24d3ff2046"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6309), false, null, null, "9" },
                    { new Guid("1c092bfb-41de-40c1-8598-407e29b532f1"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6308), false, null, null, "8" },
                    { new Guid("12b627df-7398-47be-85d0-696f3998cd12"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6312), false, null, null, "11" },
                    { new Guid("b2234fe3-c99e-4e2c-9330-e264b281c48c"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6298), false, null, null, "6" },
                    { new Guid("00846b58-fa6b-4e37-8167-5551fb98a50e"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6297), false, null, null, "5" },
                    { new Guid("8f310324-4cf2-4df6-b2c3-f1ae3528a78e"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6295), false, null, null, "4" },
                    { new Guid("65533289-5ff8-4c02-9f30-6280d4286095"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6293), false, null, null, "3" },
                    { new Guid("e96d6026-e422-45a4-bd44-650b4af3f59a"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6291), false, null, null, "2" },
                    { new Guid("56e9ac38-5afa-4420-bfe9-5089585c2f8a"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6265), false, null, null, "1" },
                    { new Guid("c5eeb2e7-a5c4-489e-a42d-7b642b639102"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(6306), false, null, null, "7" }
                });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("1a227c52-a576-4222-8afc-424921239072"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5705) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("3087b823-1620-4e7b-87fd-b5dd45f3d5f3"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5713) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("73d81136-2647-45c7-a011-018f6bed2c8a"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5710) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("8296c7a7-62e5-4ffa-bcda-8eef49dbb433"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5663) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("b24a381b-d887-4af2-a3db-cd1226e03e86"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5698) });

            migrationBuilder.UpdateData(
                table: "GuardianTypes",
                keyColumn: "Id",
                keyValue: new Guid("b9946f91-8e24-492f-9656-33fe2feeebc2"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(5702) });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "ContactPerson", "CreatedById", "CreatedDate", "Email", "ImageUrl", "IsDeleted", "ModifiedById", "ModifiedDate", "Name", "PhoneNumber", "RegistrationNo", "VatNo" },
                values: new object[] { new Guid("870d64c4-da18-41c3-8bcb-9c7c5d8b13e4"), "Ted Coffin", new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 14, 26, 49, 794, DateTimeKind.Local).AddTicks(1198), "tedc@destinychangeracademy.co.za", null, false, null, null, "Destiny Changers Academy", "0824693520", "", "" });

            migrationBuilder.InsertData(
                table: "Vat",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ValidFrom", "ValidTo", "Value" },
                values: new object[] { new Guid("f42a336b-2aad-4a5b-8315-630134560876"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 14, 26, 49, 794, DateTimeKind.Local).AddTicks(9691), false, null, null, null, null, 15m });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountStatuses",
                keyColumn: "Id",
                keyValue: new Guid("55b35573-7d5c-4142-8c7f-9e1ce1425898"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(2139) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountStatuses",
                keyColumn: "Id",
                keyValue: new Guid("de4649fd-e49d-43f7-9e04-a779cfade4ae"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(2173) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("068cd348-f51f-4d5e-b265-5ff8db1de74b"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(3960) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("23ec936b-c364-4e7b-bffd-52dffa0b4c94"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(3912) });

            migrationBuilder.UpdateData(
                schema: "billing",
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: new Guid("9ceda47f-c8ba-4af3-a9e0-dbf4adbe80b1"),
                columns: new[] { "CreatedById", "CreatedDate" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(3875) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new Guid("7de61f4a-cae0-4b22-b661-ad7ce4ba6a68") });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "CountryId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("6f242042-db42-42ee-a6b7-1f2632d69fd2"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7643), false, null, null, "Eastern Cape" },
                    { new Guid("3f7af9e3-123b-420e-8d5a-682ef4447359"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7674), false, null, null, "Free state" },
                    { new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7676), false, null, null, "Gauteng" },
                    { new Guid("538ef536-482e-47c8-aefd-81edd66712fd"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7678), false, null, null, "KwaZulu-Natal" },
                    { new Guid("4f7ac751-ba4e-413f-a6de-cb0e1c6c38dc"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7716), false, null, null, "Limpopo" },
                    { new Guid("3265a53a-e153-4145-ae18-b7bf1c18a1b7"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7718), false, null, null, "Mpumalanga" },
                    { new Guid("05fbcee1-dbb8-478c-9120-a79d5a2b48e2"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7719), false, null, null, "North West" },
                    { new Guid("3731fb9c-1af8-49db-a3a0-490e87457352"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7721), false, null, null, "Northern Cape" },
                    { new Guid("ba2191fd-c65a-45a2-953a-16c54345a9e1"), new Guid("2e976e38-314c-40dd-a8f1-a7fc711d16ed"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(7723), false, null, null, "Western Cape" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { new Guid("ba851696-0c7e-4f2f-95a6-42b8c577f50a"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8511), false, null, null, "Alexandra", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("c0407c47-e3c1-4186-ac02-a713313d9746"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8599), false, null, null, "Randfontein", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("5c2571be-ef1c-4675-917e-6acfe1d38818"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8597), false, null, null, "Magaliesburg", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("001b5ad8-a911-44f9-8368-db87ee7e53fc"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8595), false, null, null, "Krugersdorp", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("c4cf1032-3067-4a36-948b-b282119542dc"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8594), false, null, null, "Heidelberg", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("a2569574-531e-42f1-8d6f-5ffc17a0b111"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8592), false, null, null, "Meyerton", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("eca79886-9f2e-4123-b7e6-7154cd453a81"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8591), false, null, null, "Vereeniging", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("c204e515-c218-4241-ad7f-83839b816e82"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8589), false, null, null, "Vanderbijlpark", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("dc919018-beca-4fd6-b81c-2ee3b05fdd48"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8588), false, null, null, "Pretoria", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("da969129-bd50-43fb-9cf1-94e444ad2b30"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8585), false, null, null, "Irene", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("8b1cb13d-e815-4117-a7e9-0e74293ca7d2"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8582), false, null, null, "Hammanskraal", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("fdc3375b-89df-447e-a794-b65aae1ec8f4"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8581), false, null, null, "Centurion", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("7893f447-c8ab-4510-82ee-83541ceaa063"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8579), false, null, null, "Tembisa", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("15b8eac7-e3ba-4e07-a046-268e4562dfd6"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8578), false, null, null, "Springs", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("4c319d10-1bc8-4357-9f1e-e6a8cc77a91c"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8576), false, null, null, "Reiger Park", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("df0a6b64-fdb2-4857-8da6-6be643b23b9c"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8575), false, null, null, "Nigel", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("b5b3d54c-78c8-4834-b002-68dccc7eca72"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8573), false, null, null, "Kempton Park", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("4ee8cf0f-3920-4d55-b6df-251e150bd399"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8570), false, null, null, "Isando", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("dd8b3ac3-fa38-4a3d-aee1-3ac73bb2ee52"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8541), false, null, null, "Johannesburg", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("722abd8b-0245-4e86-ac75-3001914a4c0f"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8544), false, null, null, "Lenasia", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("715a7ad4-1752-42d5-bce7-d34d0b71aa8a"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8546), false, null, null, "Midrand", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("6cd02125-8b73-46eb-9ecb-ca1deaf213f1"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8548), false, null, null, "Randburg", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("4bed07fa-e936-410d-afdc-bbc60bc9d67b"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8549), false, null, null, "Roodepoort", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("1ba9da78-2ff7-4f85-8bf2-aae2ebd45c9d"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8551), false, null, null, "Sandton", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("2b87a894-b7c0-4fe9-9e07-6f5ef9361aca"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8602), false, null, null, "Bekkersdal", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("69b02534-4c59-40ac-b0a9-08d96259ce6d"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8553), false, null, null, "Soweto", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("a610fad8-9cba-4889-ade0-5a78c8b9ccdd"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8560), false, null, null, "Benoni", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("da5304c0-3b47-43a5-a491-6998df095e2e"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8562), false, null, null, "Boksburg", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("c572687c-0c05-43eb-a124-d57007daed76"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8563), false, null, null, "Brakpan", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("8f32da1a-7084-4689-a0d5-9a2fea8894c0"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8565), false, null, null, "Daveyton", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("f9afae8a-5a69-4399-bb56-772fe92f096e"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8566), false, null, null, "Edenvale", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("83d9d0dd-d1c1-4176-8523-d7da9f11b2a4"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8568), false, null, null, "Germiston", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("28960afc-caa9-4900-9b7b-0a048b918553"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8558), false, null, null, "Alberton", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") },
                    { new Guid("f506e027-5c52-4822-8f86-0c0281e8962a"), new Guid("171095d1-fc7c-4348-a834-b801621fbcad"), new DateTime(2020, 11, 5, 12, 26, 49, 794, DateTimeKind.Utc).AddTicks(8604), false, null, null, "Westonaria", new Guid("c79009f3-33cc-4378-acaa-a173bfbb236d") }
                });
        }
    }
}
