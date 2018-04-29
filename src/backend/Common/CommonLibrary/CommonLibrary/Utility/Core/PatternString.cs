using Enums.Miscellaneous;
 

namespace Utility.Core
{
    internal static class PatternString
    {
        internal static string GetPattern(PatternType type)
        {
            string pattern = string.Empty;
            switch (type)
            {
                case PatternType.USA:
                case PatternType.Canada:
                    pattern = @"^\+?(1)?-?\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
                    break;
                case PatternType.UK:
                    pattern = @"^\+?(44)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.India:
                    pattern = @"^\+?(91)?\s?[789]\d{9}$";
                    break;
                case PatternType.Australia:
                    pattern = @"^\+?(61)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Afghanistan:
                    pattern = @"^\+?(93)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.AmericanSamoa:
                    pattern = @"^\+?(1)?\s?(684)\s?\d{7}$";
                    break;
                case PatternType.Albania:
                    pattern = @"^\+?(355)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Algeria:
                    pattern = @"^\+?(213)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Andorra:
                    pattern = @"^\+?(376)?\s?\d{6}$";
                    break;
                case PatternType.Angola:
                    pattern = @"^\+?(244)?\s?\d{9}$";
                    break;
                case PatternType.Anguilla:
                    pattern = @"^\+?(264)?\s?\d{7}$";
                    break;
                case PatternType.Antigua:
                case PatternType.Barbuda:
                    pattern = @"^\+?(268)?\s?\d{7}$";
                    break;
                case PatternType.Argentina:
                    pattern = @"^\+?(54)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.Armenia:
                    pattern = @"^\+?(374)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Aruba:
                    pattern = @"^\+?(297)?\s?\d{7}$";
                    break;
                case PatternType.AscensionIsland:
                    pattern = @"^\+?(247)?\s?\d{4}$";
                    break;
                case PatternType.Austria:
                    pattern = @"^\+?(43)?\s?[123456789]\d{3,12}$";
                    break;
                case PatternType.Azerbaijan:
                    pattern = @"^\+?(994)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Bahamas:
                    pattern = @"^\+?(1)?\s?(242)\d{7}$";
                    break;
                case PatternType.Bahrain:
                    pattern = @"^\+?(973)?\s?\d{8}$";
                    break;
                case PatternType.Bangladesh:
                    pattern = @"^\+?(880)?\s?[123456789]\d{6,9}$";
                    break;
                case PatternType.Barbados:
                    pattern = @"^\+?(1)?\s?(246)\d{7}$";
                    break;
                case PatternType.Belarus:
                    pattern = @"^\+?(375)?\s?\d{9}$";
                    break;
                case PatternType.Belgium:
                    pattern = @"^\+?(32)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Belize:
                    pattern = @"^\+?(501)?\s?\d{7}$";
                    break;
                case PatternType.Benin:
                    pattern = @"^\+?(229)?\s?\d{8}$";
                    break;
                case PatternType.Bhutan:
                    pattern = @"^\+?(975)?\s?\d{7,8}$";
                    break;
                case PatternType.Bolivia:
                    pattern = @"^\+?(591)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Bosnia:
                    pattern = @"^\+?(387)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Botswana:
                    pattern = @"^\+?(267)?\s?\d{7,8}$";
                    break;
                case PatternType.Brazil:
                    pattern = @"^\+?(55)?\s?[123456789]\d{9,10}$";
                    break;
                case PatternType.BritishVirginIslands:
                    pattern = @"^\+?(1)?\s?(284)\d{7}$";
                    break;
                case PatternType.Brunei:
                    pattern = @"^\+?(673)?\s?\d{7}$";
                    break;
                case PatternType.Bulgaria:
                    pattern = @"^\+?(359)?\s?[123456789]\d{6,8}$";
                    break;
                case PatternType.BurkinaFaso:
                    pattern = @"^\+?(226)?\s?\d{8}$";
                    break;
                case PatternType.Burundi:
                    pattern = @"^\+?(257)?\s?(22)\d{6}$";
                    break;
                case PatternType.Bermuda:
                    pattern = @"^\+?(1)?\s?(441)\d{7}$";
                    break;
                case PatternType.Cambodia:
                    pattern = @"^\+?(855)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Cameroon:
                    pattern = @"^\+?(237)?\s?\d{9}$";
                    break;
                case PatternType.CapeVerde:
                    pattern = @"^\+?(238)?\s?\d{7}$";
                    break;
                case PatternType.CaymanIslands:
                    pattern = @"^\+?(1)?\s?(345)\d{7}$";
                    break;
                case PatternType.CentralAfricanRepublic:
                    pattern = @"^\+?(236)?\s?\d{8}$";
                    break;
                case PatternType.Chad:
                    pattern = @"^\+?(235)?\s?\d{8}$";
                    break;
                case PatternType.Chile:
                    pattern = @"^\+?(56)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.China:
                    pattern = @"^\+?(86)?\s?[123456789]\d{7,11}$";
                    break;
                case PatternType.Colombia:
                    pattern = @"^\+?(57)?\s?[123456789]\d{7,9}$";
                    break;
                case PatternType.Comoros:
                    pattern = @"^\+?(269)?\s?[123456789]\d{6}$";
                    break;
                case PatternType.Congo:
                    pattern = @"^\+?(242)?\s?\d{9}$";
                    break;
                case PatternType.CongoDemocraticRepublic:
                    pattern = @"^\+?(242)?\s?[123456789]\d{6,8}$";
                    break;
                case PatternType.CookIslands:
                    pattern = @"^\+?(682)?\s?\d{5}$";
                    break;
                case PatternType.CostaRica:
                    pattern = @"^\+?(506)?\s?\d{8}$";
                    break;
                case PatternType.IvoryCoast:
                    pattern = @"^\+?(225)?\s?\d{8}$";
                    break;
                case PatternType.Croatia:
                    pattern = @"^\+?(385)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Cuba:
                    pattern = @"^\+?(53)?\s?[123456789]\d{5,7}$";
                    break;
                case PatternType.Curacao:
                    pattern = @"^\+?(599)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Cyprus:
                    pattern = @"^\+?(357)?\s?\d{8}$";
                    break;
                case PatternType.CzechRepublic:
                    pattern = @"^\+?(420)?\s?\d{9}$";
                    break;
                case PatternType.Denmark:
                    pattern = @"^\+?(45)?\s?\d{8}$";
                    break;
                case PatternType.DiegoGarcia:
                    pattern = @"^\+?(246)?\s?\d{7}$";
                    break;
                case PatternType.Djibouti:
                    pattern = @"^\+?(253)?\s?\d{8}$";
                    break;
                case PatternType.Dominica:
                    pattern = @"^\+?(1)?\s?(767)\d{7}$";
                    break;
                case PatternType.DominicanRepublic:
                    pattern = @"^\+?(1)?\s?(809)\d{7}$"; //809/829/849 
                    break;
                case PatternType.EastTimor:
                    pattern = @"^\+?(670)?\s?(767)\d{7,8}$";
                    break;
                case PatternType.Ecuador:
                    pattern = @"^\+?(593)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Egypt:
                    pattern = @"^\+?(20)?\s?[123456789]\d{7,9}$";
                    break;
                case PatternType.ElSalvador:
                    pattern = @"^\+?(503)?\s?(2)\d{7}$";
                    break;
                case PatternType.EquatorialGuinea:
                    pattern = @"^\+?(240)?\s?\d{9}$";
                    break;
                case PatternType.Eritrea:
                    pattern = @"^\+?(291)?\s?[123456789]\d{6}$";
                    break;
                case PatternType.Estonia:
                    pattern = @"^\+?(372)?\s?[123456789]\d{6}$";
                    break;
                case PatternType.Ethiopia:
                    pattern = @"^\+?(251)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.FalklandIslands:
                    pattern = @"^\+?(500)?\s?\d{5}$";
                    break;
                case PatternType.FaroeIslands:
                    pattern = @"^\+?(298)?\s?\d{6}$";
                    break;
                case PatternType.Fiji:
                    pattern = @"^\+?(679)?\s?\d{7}$";
                    break;
                case PatternType.Finland:
                    pattern = @"^\+?(358)?\s?[123456789]\d{4,11}$";
                    break;
                case PatternType.France:
                    pattern = @"^\+?(33)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.FrenchGuiana:
                    pattern = @"^\+?(594)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.FrenchPolynesia:
                    pattern = @"^\+?(689)?\s?\d{6}$";
                    break;
                case PatternType.Gabon:
                    pattern = @"^\+?(241)?\s?\d{7}$";
                    break;
                case PatternType.Gambia:
                    pattern = @"^\+?(220)?\s?\d{7}$";
                    break;
                case PatternType.Georgia:
                    pattern = @"^\+?(995)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Germany:
                    pattern = @"^\+?(49)?\s?[123456789]\d{4,10}$";
                    break;
                case PatternType.Ghana:
                    pattern = @"^\+?(233)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Gibraltar:
                    pattern = @"^\+?(350)?\s?\d{8}$";
                    break;
                case PatternType.Greece:
                    pattern = @"^\+?(30)?\s?\d{10}$";
                    break;
                case PatternType.Greenland:
                    pattern = @"^\+?(299)?\s?\d{6}$";
                    break;
                case PatternType.Grenada:
                    pattern = @"^\+?(1)?\s?(473)\d{7}$";
                    break;
                case PatternType.Guadeloupe:
                    pattern = @"^\+?(590)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Guam:
                    pattern = @"^\+?(1)?\s?(671)\d{7}$";
                    break;
                case PatternType.Guatemala:
                    pattern = @"^\+?(502)?\s?\d{8}$";
                    break;
                case PatternType.GuineaBissau:
                    pattern = @"^\+?(245)?\s?\d{7}$";
                    break;
                case PatternType.Guinea:
                    pattern = @"^\+?(224)?\s?\d{8}$";
                    break;
                case PatternType.Guyana:
                    pattern = @"^\+?(592)?\s?\d{7}$";
                    break;
                case PatternType.Haiti:
                    pattern = @"^\+?(509)?\s?\d{8}$";
                    break;
                case PatternType.Honduras:
                    pattern = @"^\+?(504)?\s?\d{8}$";
                    break;
                case PatternType.HongKong:
                    pattern = @"^\+?(852)?\s?\d{8}$";
                    break;
                case PatternType.Hungary:
                    pattern = @"^\+?(36)?\s?\d{8,9}$";
                    break;
                case PatternType.Iceland:
                    pattern = @"^\+?(354)?\s?[45]\d{6}$";
                    break;
                case PatternType.Indonesia:
                    pattern = @"^\+?(62)?\s?[123456789]\d{6,10}$";
                    break;
                case PatternType.Inmarsat:
                    pattern = @"^\+?(870)?\s?\d{9}$";
                    break;
                case PatternType.Iran:
                    pattern = @"^\+?(98)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.Iraq:
                    pattern = @"^\+?(964)?\s?\d{8,10}$";
                    break;
                case PatternType.Ireland:
                    pattern = @"^\+?(353)?\s?[123456789]\d{6,8}$";
                    break;
                case PatternType.Iridium:
                    pattern = @"^\+?(8816)?\s?\d{8}$";
                    break;
                case PatternType.Israel:
                    pattern = @"^\+?(972)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Italy:
                    pattern = @"^\+?(39)?\s?\d{6,10}$";
                    break;
                case PatternType.Jamaica:
                    pattern = @"^\+?(1)?\s?(876)\d{7}$";
                    break;
                case PatternType.Japan:
                    pattern = @"^\+?(81)?\s?[123456789]\d{8,9}$";
                    break;
                case PatternType.Jordan:
                    pattern = @"^\+?(962)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Kazakhstan:
                    pattern = @"^\+?(7)?\s?\d{10}$";
                    break;
                case PatternType.Kenya:
                    pattern = @"^\+?(254)?\s?[123456789]\d{5,8}$";
                    break;
                case PatternType.Kiribati:
                    pattern = @"^\+?(686)?\s?\d{5}$";
                    break;
                case PatternType.NorthKorea:
                    pattern = @"^\+?(850)?\s?\d{7}$";
                    break;
                case PatternType.SouthKorea:
                    pattern = @"^\+?(82)?\s?[123456789]\d{5,11}$";
                    break;
                case PatternType.Kuwait:
                    pattern = @"^\+?(965)?\s?\d{8}$";
                    break;
                case PatternType.Kyrgyzstan:
                    pattern = @"^\+?(996)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Laos:
                    pattern = @"^\+?(856)?\s?[123456789]\d{7,9}$";
                    break;
                case PatternType.Latvia:
                    pattern = @"^\+?(371)?\s?\d{8}$";
                    break;
                case PatternType.Lebanon:
                    pattern = @"^\+?(961)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Lesotho:
                    pattern = @"^\+?(266)?\s?\d{8}$";
                    break;
                case PatternType.Liberia:
                    pattern = @"^\+?(231)?\s?\d{7,8}$";
                    break;
                case PatternType.Libya:
                    pattern = @"^\+?(218)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Liechtenstein:
                    pattern = @"^\+?(423)?\s?\d{7}$";
                    break;
                case PatternType.Lithuania:
                    pattern = @"^\+?(370)?\s?\d{8}$";
                    break;
                case PatternType.Luxembourg:
                    pattern = @"^\+?(352)?\s?\d{5,11}$";
                    break;
                case PatternType.Macau:
                    pattern = @"^\+?(853)?\s?\d{8}$";
                    break;
                case PatternType.Macedonia:
                    pattern = @"^\+?(389)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Madagascar:
                    pattern = @"^\+?(261)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Malawi:
                    pattern = @"^\+?(265)?\s?\d{7,9}$";
                    break;
                case PatternType.Malaysia:
                    pattern = @"^\+?(60)?\s?\d{8,10}$";
                    break;
                case PatternType.Maldives:
                    pattern = @"^\+?(960)?\s?\d{7}$";
                    break;
                case PatternType.Mali:
                    pattern = @"^\+?(223)?\s?\d{8}$";
                    break;
                case PatternType.Malta:
                    pattern = @"^\+?(356)?\s?\d{8}$";
                    break;
                case PatternType.MarshallIslands:
                    pattern = @"^\+?(692)?\s?\d{7}$";
                    break;
                case PatternType.Martinique:
                    pattern = @"^\+?(596)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Mauritania:
                    pattern = @"^\+?(222)?\s?\d{8}$";
                    break;
                case PatternType.Mauritius:
                    pattern = @"^\+?(230)?\s?\d{7,8}$";
                    break;
                case PatternType.MayotteIsland:
                    pattern = @"^\+?(262)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Mexico:
                    pattern = @"^\+?(52)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.Micronesia:
                    pattern = @"^\+?(691)?\s?\d{7}$";
                    break;
                case PatternType.Moldova:
                    pattern = @"^\+?(373)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Monaco:
                    pattern = @"^\+?(377)?\s?\d{8}$";
                    break;
                case PatternType.Mongolia:
                    pattern = @"^\+?(976)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Montenegro:
                    pattern = @"^\+?(382)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Montserrat:
                    pattern = @"^\+?(664)?\s?\d{7}$";
                    break;
                case PatternType.Morocco:
                    pattern = @"^\+?(212)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Mozambique:
                    pattern = @"^\+?(258)?\s?\d{8,9}$";
                    break;
                case PatternType.Myanmar:
                    pattern = @"^\+?(95)?\s?[123456789]\d{5,9}$";
                    break;
                case PatternType.Namibia:
                    pattern = @"^\+?(264)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Nauru:
                    pattern = @"^\+?(674)?\s?\d{7}$";
                    break;
                case PatternType.Nepal:
                    pattern = @"^\+?(977)?\s?[123456789](\d{7}|\d{9})$";
                    break;
                case PatternType.Netherlands:
                    pattern = @"^\+?(31)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.CaribbeanNetherlands:
                    pattern = @"^\+?(599)?\s?[123456789]\d{6}$";
                    break;
                case PatternType.NewCaledonia:
                    pattern = @"^\+?(687)?\s?\d{6}$";
                    break;
                case PatternType.NewZealand:
                    pattern = @"^\+?(64)?\s?[123456789]\d{7,9}$";
                    break;
                case PatternType.Nicaragua:
                    pattern = @"^\+?(505)?\s?\d{8}$";
                    break;
                case PatternType.Niger:
                    pattern = @"^\+?(227)?\s?\d{8}$";
                    break;
                case PatternType.Nigeria:
                    pattern = @"^\+?(234)?\s?[123456789](\d{6,7}|\d{9})$";
                    break;
                case PatternType.Niue:
                    pattern = @"^\+?(683)?\s?\d{4}$";
                    break;
                case PatternType.NorfolkIsland:
                    pattern = @"^\+?(6723)?\s?\d{5}$";
                    break;
                case PatternType.NorthernMarianaIslands:
                    pattern = @"^\+?(1)?\s?(670)\d{7}$";
                    break;
                case PatternType.Norway:
                    pattern = @"^\+?(47)?\s?\d{8}$";
                    break;
                case PatternType.Oman:
                    pattern = @"^\+?(968)?\s?\d{8}$";
                    break;
                case PatternType.Pakistan:
                    pattern = @"^\+?(92)?\s?[123456789]\d{8,9}$";
                    break;
                case PatternType.Palau:
                    pattern = @"^\+?(680)?\s?\d{7}$";
                    break;
                case PatternType.Palestine:
                    pattern = @"^\+?(970)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Panama:
                    pattern = @"^\+?(507)?\s?\d{7,8}$";
                    break;
                case PatternType.PapuaNewGuinea:
                    pattern = @"^\+?(675)?\s?\d{7}$";
                    break;
                case PatternType.Paraguay:
                    pattern = @"^\+?(595)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Peru:
                    pattern = @"^\+?(51)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Philippines:
                    pattern = @"^\+?(63)?\s?[123456789]\d{7,9}$";
                    break;
                case PatternType.Poland:
                    pattern = @"^\+?(48)?\s?\d{9}$";
                    break;
                case PatternType.Portugal:
                    pattern = @"^\+?(351)?\s?\d{9}$";
                    break;
                case PatternType.PuertoRico:
                    pattern = @"^\+?(1)?\s?(787|939)\d{7}$";
                    break;
                case PatternType.Qatar:
                    pattern = @"^\+?(974)?\s?\d{8}$";
                    break;
                case PatternType.ReunionIsland:
                    pattern = @"^\+?(262)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Romania:
                    pattern = @"^\+?(40)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Russian:
                    pattern = @"^\+?(7)?\s?\d{10}$";
                    break;
                case PatternType.Rwanda:
                    pattern = @"^\+?(250)?\s?\d{9}$";
                    break;
                case PatternType.SaintHelena:
                    pattern = @"^\+?(290)?\s?\d{5}$";
                    break;
                case PatternType.SaintKitts:
                    pattern = @"^\+?(1)?\s?(869)\d{7}$";
                    break;
                case PatternType.SaintLucia:
                    pattern = @"^\+?(1)?\s?(758)\d{7}$";
                    break;
                case PatternType.SaintPierre:
                    pattern = @"^\+?(508)?\s?\d{6}$";
                    break;
                case PatternType.SaintVincent:
                    pattern = @"^\+?(1)?\s?(784)\d{7}$";
                    break;
                case PatternType.Samoa:
                    pattern = @"^\+?(685)?\s?\d{5}$";
                    break;
                case PatternType.SanMarino:
                    pattern = @"^\+?(378)?\s?\d{10}$";
                    break;
                case PatternType.SaoTome:
                    pattern = @"^\+?(239)?\s?\d{7}$";
                    break;
                case PatternType.SaudiArabia:
                    pattern = @"^\+?(966)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Senegal:
                    pattern = @"^\+?(221)?\s?\d{9}$";
                    break;
                case PatternType.Serbia:
                    pattern = @"^\+?(381)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Seychelles:
                    pattern = @"^\+?(248)?\s?\d{7}$";
                    break;
                case PatternType.SierraLeone:
                    pattern = @"^\+?(232)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.Singapore:
                    pattern = @"^\+?(65)?\s?\d{8}$";
                    break;
                case PatternType.SintMaarten:
                    pattern = @"^\+?(1)?\s?(721)\d{7}$";
                    break;
                case PatternType.Slovakia:
                    pattern = @"^\+?(421)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Slovenia:
                    pattern = @"^\+?(386)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.SolomonIslands:
                    pattern = @"^\+?(677)?\s?\d{5}$";
                    break;
                case PatternType.Somalia:
                    pattern = @"^\+?(252)?\s?\d{7}$";
                    break;
                case PatternType.SouthAfrica:
                    pattern = @"^\+?(27)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.SouthSudan:
                    pattern = @"^\+?(211)?\s?\d{9}$";
                    break;
                case PatternType.Spain:
                    pattern = @"^\+?(34)?\s?\d{9}$";
                    break;
                case PatternType.SriLanka:
                    pattern = @"^\+?(94)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Sudan:
                    pattern = @"^\+?(249)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Suriname:
                    pattern = @"^\+?(597)?\s?[123456789]\d{5}$";
                    break;
                case PatternType.Swaziland:
                    pattern = @"^\+?(268)?\s?\d{8}$";
                    break;
                case PatternType.Sweden:
                    pattern = @"^\+?(46)?\s?[123456789]\d{6,8}$";
                    break;
                case PatternType.Switzerland:
                    pattern = @"^\+?(41)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Syria:
                    pattern = @"^\+?(963)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Taiwan:
                    pattern = @"^\+?(886)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Tajikistan:
                    pattern = @"^\+?(992)?\s?\d{9}$";
                    break;
                case PatternType.Tanzania:
                    pattern = @"^\+?(225)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Thailand:
                    pattern = @"^\+?(66)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Thuraya:
                    pattern = @"^\+?(88216)?\s?\d{8}$";
                    break;
                case PatternType.Togo:
                    pattern = @"^\+?(228)?\s?\d{8}$";
                    break;
                case PatternType.Tokelau:
                    pattern = @"^\+?(690)?\s?\d{4}$";
                    break;
                case PatternType.Tonga:
                    pattern = @"^\+?(676)?\s?\d{5}$";
                    break;
                case PatternType.TrinidadTobago:
                    pattern = @"^\+?(1)?\s?(868)\d{7}$";
                    break;
                case PatternType.Tunisia:
                    pattern = @"^\+?(216)?\s?\d{8}$";
                    break;
                case PatternType.Turkey:
                    pattern = @"^\+?(90)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.Turkmenistan:
                    pattern = @"^\+?(993)?\s?\d{8}$";
                    break;
                case PatternType.TurksCaicosIslands:
                    pattern = @"^\+?(1)?\s?(649)\d{7}$";
                    break;
                case PatternType.Tuvalu:
                    pattern = @"^\+?(688)?\s?\d{5}$";
                    break;
                case PatternType.Uganda:
                    pattern = @"^\+?(256)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Ukraine:
                    pattern = @"^\+?(380)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.UAE:
                    pattern = @"^\+?(971)?\s?[123456789]\d{7,8}$";
                    break;
                case PatternType.Uruguay:
                    pattern = @"^\+?(598)?\s?[123456789]\d{7}$";
                    break;
                case PatternType.USVirginIslands:
                    pattern = @"^\+?(1)?\s?(340)\d{7}$";
                    break;
                case PatternType.Uzbekistan:
                    pattern = @"^\+?(998)?\s?\d{9}$";
                    break;
                case PatternType.Vanuatu:
                    pattern = @"^\+?(678)?\s?\d{5}$";
                    break;
                case PatternType.VaticanCity:
                    pattern = @"^\+?(379)?\s?\d{6,10}$";
                    break;
                case PatternType.Venezuela:
                    pattern = @"^\+?(58)?\s?[123456789]\d{9}$";
                    break;
                case PatternType.Vietnam:
                    pattern = @"^\+?(84)?\s?[123456789]\d{8,9}$";
                    break;
                case PatternType.WallisFutuna:
                    pattern = @"^\+?(681)?\s?\d{6}$";
                    break;
                case PatternType.Yemen:
                    pattern = @"^\+?(967)?\s?[123456789]\d{6,8}$";
                    break;
                case PatternType.Zambia:
                    pattern = @"^\+?(260)?\s?[123456789]\d{8}$";
                    break;
                case PatternType.Zimbabwe:
                    pattern = @"^\+?(263)?\s?[123456789]\d{4,9}$";
                    break;
                case PatternType.Universal:
                    pattern = @"^(\+\d{1,3} ?)?(\(\d{1,5}\)|\d{1,5}) ?\d{3} ?\d{0,7}( (x|xtn|ext|extn|pax|pbx|extension)?\.? ?\d{2-5})?$";
                    break;
            }
            return pattern;
        }
        internal static System.Text.RegularExpressions.Regex GetRegexInstance(PatternType type)
        {

            return new System.Text.RegularExpressions.Regex(GetPattern(type), System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);
        }
    }
}
