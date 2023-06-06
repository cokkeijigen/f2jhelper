using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace F2JHelper
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (Init.path.Length == 0)
                return;
            string[] conf = Init.conf;
            conf[1] = Init.path;
            conf[5] = Init.path + "_out";
            this.textBox1.Lines = Init.conf;
        }

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private Dictionary<char, char> createMaps(int val)
        {
            var result = new Dictionary<char, char>();
            char[] ftchars = ft_chars.ToCharArray();
            char[] jtchars = jt_chars.ToCharArray();
            if (val == 1)
            {
                for (int i = 0; i < ftchars.Length; i++)
                    result.TryAdd(ftchars[i], jtchars[i]);
            }
            else if (val == 2)
            {
                for (int i = 0; i < jtchars.Length; i++)
                    result.TryAdd(jtchars[i], ftchars[i]);
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //AllocConsole();
            string[] conf = this.textBox1.Lines;
            if (conf == null || conf.Length != 12)
            {
                MessageBox.Show("²ÎÊýÐÅÏ¢´íÎó»òÕßÎª¿Õ£¡");
            }
            else
            {
                try
                {
                    int conversion = Int16.Parse(conf[9].Trim());
                    string inputPath = conf[1].Trim();
                    string inputEncode = conf[2].Trim();
                    string outputPath = conf[5].Trim();
                    string outputEncode = conf[6].Trim();
                    Dictionary<char, char> maps = createMaps(conversion);
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    Encoding I_encode = Encoding.GetEncoding(inputEncode);
                    Encoding O_encode = Encoding.GetEncoding(outputEncode);
                    this.textBox1.AppendText(Environment.NewLine);
                    string fileName = "";
                    foreach (string path in Directory.GetFiles(inputPath))
                    {
                        fileName = path.Substring(path.LastIndexOf("\\") + 1);
                        try
                        {
                            string fileData = File.ReadAllText(path, I_encode);
                            foreach (KeyValuePair<char, char> pair in maps)
                            {
                                fileData = fileData.Replace(pair.Key, pair.Value);
                            }
                            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
                            File.WriteAllText(outputPath + "\\" + fileName, fileData, O_encode);
                            this.textBox1.AppendText(Environment.NewLine + fileName + ": done");
                        }
                        catch (Exception ex)
                        {
                            this.textBox1.AppendText(Environment.NewLine + fileName + ": " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }


        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e != null && e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length != 1)
                {
                    MessageBox.Show("²»Ö§³Ö¶à¸ö²ÎÊý£¡");
                }
                else
                {
                    string[] conf = Init.conf;
                    conf[1] = files[0];
                    conf[5] = files[0] + "_out";
                    this.textBox1.Lines = conf;
                }
            }

        }

        private static string ft_chars = "°}Ì@µKÛóaÂOÒ\ŠW‰ÎÁT”[”¡îCÞk½OŽÍ½‰æ^Ör„ƒï–ŒšˆóõUÝ…Øä^ªN‚ä‘v¿‡¹P®…”ÀŽÅé]ß…¾ŽÙH×ƒÞqÞp˜Ë÷M„e°TžlžIÙe”PïžK“ÜÀãKñgÊNÑaØ”…¢ÐQšˆ‘M‘K NÉnÅ“‚}œæŽú‚ÈƒÔœyŒÓÔŒ”v“½Ïsð’×‹ÀpçP®aêUîˆö‡LéLƒ”ÄcS•³ânÜ‡Ø‰mÉòêÒr“Î·Q‘ÍÕ\òG°VßtñYuýXŸë›_ÏxŒ™® ÜP»I¾Iáh™»NäzërµAƒ¦Ó|ÌŽ‚÷¯êJ„“åN¼ƒ¾bÞoÔ~ÙnÂ”Ê[‡èÄ…²œÜf¸Zåeß_Ž§ÙJ“ú†Îà“ÛÄ‘‘„ÕQ—®”“õühÊŽ™n“vu¶\Œ§±IŸôà‡”³œìßf¾†îüc‰|ëŠÕµñážÕ{µþÕ™¯Bá”í”åVÓ†G–|„Ó—ƒöôY Ùªš×xÙ€åƒå‘”à¾„ƒ¶ê Œ¦‡îDâgŠZ‰™ùZî~ÓžºðIƒº –ðDÙE°lÁPéy¬mµ\âCŸ©¹ ØœïˆÔL¼ïwÕuUÙM¼Š‰žŠ^‘¼SØS—÷ähïL¯‚ñT¿pÖSøPÄwÝ—“áÝoÙxÍØ“Ó‡‹D¿`Ô“â}ÉwŽÖ—UÚs¶’ÚMŒù„‚ä“¾VÅVæ€”Røéwãt‚€½oýŒmì–Ø•ã^œÏÆˆ˜‹Ù‰òÐMî™„Ž’ìêPÓ^ð^‘TØžVÒŽÎùšwý”é|Ü‰ÔŽ™™ÙF„£ÝLå‡øß^ñ”ínhÌ–éuúQÙR™MÞZø™¼táá‰Ø×oœû‘ô‡WÈA®‹„Ô’‘Ñ‰Äšg­hß€¾“Q†¾¯ˆŸ¨œoüSÖe“]Ýxš§ÙV·x•þ Z…RÖMÕdÀLÈœ†â·«@Ø›µœ“ô™C·eð‡ÛE×Iëu¿ƒ¾ƒ˜OÝ‹¼‰”DŽ×ËE„©úÓ‹Ó›ëHÀ^¼oŠAÇvîaÙZâ›ƒrñ{šž±OˆÔ¹{égÆD¾}ÀO™z‰Aû|’þ“ìº†ƒ€œpË]™‘èbÛ`ÙvÒŠæIÅž„¦ðTužR¾Œ¢{ÊY˜ªª„ÖváuÄz²òœ‹É”‡ãq³CƒeÄ_ïœÀU½gÞIÝ^·MëA¹ÇoöLó@½›îiìoçR½¯d¸‚ƒô¼mŽýÅfñxÅe“þä‘Ö„¡ùN½‚Ü½YÕ]ŒÃ¾oå\ƒHÖ”ßM•x a±M„ÅÇGÓX›QÔE½^âxÜŠòEé_„Pîwš¤Õn‰¨‘©“¸ŽìÑÕF‰Kƒ~Œ’µV•ç›rÌŽh¸Qð¢”UéŸÏžÅDÈRíÙ‡Ë{™Ú”r»@ê@Ìmž‘×Ž”ˆÓ[‘ÐÀ| €žE¬˜“Æ„Ú³˜·èD‰¾îœI»hØ‚ëxÑYõŽ¶Yû…–„îµ[švžrë`‚zÂ“ÉßBç ‘ziºŸ”¿Ä˜æœ‘ÙŸ’¾š¼Z›öƒÉÝvÕ¯Ÿß|ç‚«CÅRà÷[„CÙUýgâœRì`ŽXîIðs„¢ýˆÃ@‡µ»\‰Å”në]˜ÇŠä“§ºtÌJ±RïB] t“ïûuÌ”ô”ÙTµ“ä›ê‘óH…ÎäX‚HŒÒ¿|‘]žV¾GŽn”Œ\ž´y’àÝ†‚öœS¾]Õ“Ì}Á_ß‰èŒ»jò…ñ˜½j‹Œ¬”´aÎ›ñRÁR†áÙIûœÙuß~Ã}²mðzÐUMÖ™Øˆå^ãTÙQ÷áüq›]æVéTž‚ƒåi‰ô²[Öi›Ò’ƒç¾d¾’Rœç‘‘é}øQã‘Ö‡Ö\®€…Èâc¼{ëy“ÏÄXÀô[ðHƒÈ”MÄ”f“Óá„øBÂ™‡§è‡æ‡™ŽªŸŒŽ”Qôâo¼~Ä“âÞr¯‘ÖZšWútšª‡Ia±Pý‹’Ùr‡Šùiò_ïhîlØšÌO‘{ÔuŠîH“ää˜ã×V—«œDÄšýRòTØM†¢šâ—‰Ó™ ¿’LâFãUßwºžÖtåXãQ“œ\×l‰q˜Œ†Ü‰¦ËNŠ“Œæ@˜ò†ÌƒSÂN¸[¸`šJÓHŒ‹ÝpšäƒAí•Õˆ‘c­‚¸FÚ……^Ü|òŒýxïE™à„ñ…sùo´_×Œðˆ”_À@ŸáígÕJ¼x˜s½qÜ›äJéc™ž¢Ë_öwÙÈý‚ã†Êò}’ß­š¢„x¼†ºY•ñ„héWêƒÙ ¿˜‰„‚ûÙpŸý½BÙd”z‘ØÔO¼Œ‹ðÄIBÂ•ÀK„ÙÂ}ŽŸª{ñÔŠŒÆ•rÎgŒ×Rñ‚„ÝßmáŒï—Ò•Ô‡‰Û«F˜ÐÝ”•øÚHŒÙÐg˜äØQ”µŽ›ëpÕl¶í˜Õf´T q½zï•Â–‘ZížÔAÕb”\ÌKÔVÃCëmëS½—šqŒO“p¹S¿s¬æi«H“é”EÅ_‘B”‚Ø°cž©‰¯×TÕ„‡@œ« Cý½{Ó‘òvÖ`äRî}ówŒÏ—lÙNèFdÂ ŸNã~½yî^¶dˆD‰TˆFîjÍ‘Ã“ørñWñ„™E¸DÒmž³îBÈf¾Wífß`‡úžéžH¾SÈ”‚¥‚Î¾•ðjÖ^ÐlœØÂ„¼y·€†–®Y“ëÎœu¸CÅP†èæužõ›@Õ_ŸoÊ…Ç‰]ìF„ÕÕ`åa ÞÒuÁ•ãŠ‘ò¼šÎrÝ {‚bªMB‡˜åvõrÀwûyÙtã•éeï@ëU¬F«I¿hðWÁw‘—¾€Žûè‚àlÔ”í‘í—Ê’‡ÌäN•Ô‡[Ï…f’¶”yÃ{ÖCŒ‘žaÖxä\á…Ådƒ´›°äPÀCÌ“‡uíšÔS”¢¾wÀmÜŽ‘Òßx°_½kŒW„×ÔƒŒ¤ñZÓ–Óßd‰ºøfø††¡†Ó éŽŸŸû}‡ÀŽrîéÆG…’³Ž©ÖVòžø„—î“P¯ƒê–°WðB˜Ó¬Ž“uˆòßb¸GÖ{ËŽ ”í“˜IÈ~átãžîUßzƒx¤ÏË‡ƒ|‘›ÁxÔ„×hÕx×g®À[ÊaêŽãyï‹ë[™Ñ‹ëú—‘ªÀt¬“Îž IŸÉÏ‰ÚA·f†Ñ“í‚ò°bÛxÔœ¥ƒž‘nà]â™ªqß[ÕTì¶Ý›ô~OŠÊÅcŽZÕZ»n¶Rªz×uîAñSøxœYÞ@ˆ@†TˆA¾‰ßhîŠ¼sÜSè€Ž[»›‚é†ë…ày„òëEß\ÌNáj•žíësžÄÝd”€•ºÙÚEÅKè——¸^ØŸ“ñ„tÉÙ\Ù›¼™„žÜˆåŽél–ÅÔpýS‚ùšÖ±K”ØÝšä—£‘ð¾`ˆqŽ¤Ù~Ã›ÚwÏUÞHæNß@Ø‘á˜‚ÉÔ\æ‚ê‡’ê± ªb ŽŽ¬°Yà×C¿—ÂšˆÌ¼ˆÕI“´”SŽÃÑuÙ|œþçŠ½K·NÄ[±ŠÖaÝS°™•ƒóEØiÖTÕD T²š‡ÚÙAèTºBÔ]ñvŒ£´uÞDÙ˜¶ÇfÑbŠy‰Ñ îåFÙ˜‰‹¾YÕœÊÖøáÆÙYnÛ™¾C¿‚¿vàuÔ{½Mã@åH‡†‹Ü­a•áì\ÖOä@ùg‹‹òˆö—âZ†hâkødý_ùlÙSåQÉœ†ô§ãGº`Û‹ÆS¾œ»eòŠïRïjçSès÷Bƒ†À_™‰š›Äœè\óxôW·AðGâ“ùPâ˜ò‰üoÅåšƒŠâO‡ÏÕ~×Êr‘Ô‹Èò–Ò—¶Uç†‚tÈOé‹öK³Œ‚áÖR™Â´~ýZ—–™fä…èKï†ø|ã|ƒ‰ŽÎ×‡Æc½IÜXâAí¾Eù‡Ýzýpú\Éò‹˜ºÝ”xäSûz‡}í^ñ~½Hš—Ùy°Dº„×•´XÒd cç‹¼eÔgÖB½Ó]çCŽpâš°dã“õ öläbäA–ù…¸]ž^™³ ©ºVüt»f‘»ç…ŸõÜOèIÖ@ˆ×é‘Ü—ä~åŠù˜î€î…÷{ÕOßƒãsøõbâ[ô™¾pçšöEƒfž–øDñ€¼›½EÙŽûŸõVöváÙWŒÀ“{½C‘ß²GÕa¿cä†¼væk}ƒÙs½Žõ†Ô¾—ÓMÔbÝžâ’ådøù]úXøŽ“ÔŸ“¥ûXöŠ«E…Q„¥‹‚™uõq÷ZÐ–¾iõ…ˆå†JŽ½˜¡ÏXãxêR½WîRž®î—ÔXêHÏ üZÓÈ‡éb÷cGù–ò‘˜åçfŠJÀQæDõŒömÔœËC‡‚ÒÀD¬q•ŸÕŸðQé’â€èZÓ“Ô‘Ëj‡\‡óK­^ÓJýW´‰ÁbÏŠÜQìV÷qöaàP›Ñäeæ‰ÏuÖG¿V‘â‘ì²€úY¹aöžíd½{í\“×þú„õo°XîMõ^Ž„Ë|ð~¿NÚBÓP„q›ÜÞŸ†Ã„ìnôbøFúÔnŒÕ™ÎïZâ ä|¸MýeäŸçëh×H«k°—„’‰N÷ðæzå|ýé`â‚äDòS¾~ÝVâŽä˜îhýlçH‡¿à”‡ˆÄ’ªœóyÕEÕNà—‰¿ÀkÙL…TÊ‰‘|Â˜ºˆé€åKöHÏ“ˆÆœZž|Ùl²Aån°]»[¹™ì”Ìè|ÒhéäZ‡Z÷ã™ç„°Aö˜ÕC¿wƒ«áB‰ÈËžÉWÌy‡³ßŠóP¿r™À™µÞ]µZä‡ûZ°O¼cÜVìZ÷~÷kÌ`ŠYž‡­IššÑžÒcö–ôu¿á‘úÌA[™_ÞOÜk¾c™ôÏ|öNžgòt¾^æyúwÌdž{­‡™É–VµaƒEÊV‡DâçU¯›ÂeÏNót‰À”]‡£é‚žoœO™¾™©Þ_Ý`ÞAšÚÅFûRú˜ÆA÷|ÅLŒD™èû[èŽ‡÷ Î«MžT™åÄTæ ™°Ò@äs‡`‡O‹ß˜q„ê¿zçNî‹ö üN’Ð F‘¿å{ÁdÖk«J¶[Æìtüw¾˜¿Šéh¾‡Öƒò‡ðxš{æŸãfçtÔGâ‰öFÝ‚öTÊ\ÑUêŸÌY‡Ëî”ÜbÆr‡“Âœƒz‡ñwâSƒ®ÖŽ‘Y®TÛ˜°’Þ\¼„Á`â”Õ›ñ‰¿~‹åá•çhç’ÌIòU¾_˜´ƒí î@ö’ƒLÊn‘aòqÀ`˜ âj‹Ô™{‘êŸÍäçIçjÁuÛ„ÕV×SÊwÀR´“ÜEÜå›ºDäu“åõ›Ÿ¦ÍŽ€ÙgÏlöqÔxçé˜ÓUøzÔ¾JÝbãŒé êIâÊ‹Æ˜ïïƒÜŽVÏ”¿dãœïAÍ˜ïSšÐ¼R¿‰†ÝäC·wæ|õá‡Ó˜Š™ò~áŸ÷Xˆsš‘Óx…‡ž—®ŒÔ–Õ”žcÖu‰PÉPsÝYÙBâ‹öˆ¾R”d¼‚éVèpPñ†¾ŒæJúƒË’ðtï`æ}Öq·dÕrÉpªs†îÃêYãBöâõT•ÒãgåUí™ƒ¯ðhç|çMíwäˆ¾ŸùYêD¼gýföœ‘Qâ^âQ“»ï‚»Xüƒ‹zÄe¼w¾UÝyÕ†Ž®éœ¿¬¬|ítŸ˜õné”Èný}àwT‘“‹³ò\ù^úFðqô]­tÒ ³ˆÇ{ËWÌ\sª‹¹ú’°BÏ–¶iÜ]ËGðAóJ¾|ð‹‡^žtò”½‹—nºÒC”X¼œÀiê€œîð}ø Ô‚íœÖXãCæ›ÖoÍ÷L‰_¡÷\ˆº‹I—¿šå…˜ÚIƒ°ƒ¼×—‘ÃéZá‰ô|ðýBŸ¬ÝUú_öŽìvÖ]à’•ÏŸîÔr‡ÒŽFï‘«óA¿OÝWÙOáæ„èO¯ŽÅœãŸ°a‰LúL¿Mæv”t‡Âž]žu­‹ûW°`îWÀ›çOÊ~äBôœ‚ø‚RÕ˜ÖIÊš£ï„é“‹ž¼uÓDšeâ•ùOú–ýr™´øSüxãXàiÊ|Á‘C¼‹íyšŒšè­‘ÚŽçYñzÙ‘‡KŽ¾ºj×P¿•×dÔtá“Ö†Ýmúpœ¿b˜EÝFÙcµøcÕŠ˜ã`åP¹~òs™±—dÝTÝeÙ—úvÎ‡¿{ÜWÜUÓzæR¼q¿UÐ™½ã‡Êð‚ïDòK¿PÕŽèCÖJ¾lÝwÙD±{åOýbõ™‚ôÕŒò|öOæ—ÀyÜg÷VÓ…×vàSÃÍšëÚæ‰Åˆßˆ‰|™”Ê{È’É‰ÉO¹½éÂ“«ßå†w†U‡z‡j¾ïÅüÖoÒLŽS¼¹·ÂƒeªwûƒðNðlð€ð–Àãâð‘¬ãÝsž¹»ìžEžzµ­ŒŽôé½f¾y¬z—g—¨°¸™R™ÁÝMÜ Ù}ÄdÄLïlºýŸÁïœ¡Ãì´^L²gâbãOäyäHä{äç˜ç™åŸåuåxäžæXæ[æ“çèuè‰èd·„ù‘úBûI°[åí¯{ÄŸÒM¿‹ÂgîžÏ\üDõEõGõRõœöAöX÷aöcö…öš÷IíXíxýO";
        private static string jt_chars = "°¨°ª°­°®°¹°¿°À°Â°Ó°Õ°Ú°Ü°ä°ì°í°ï°ó°÷°ù°þ±¥±¦±¨±«±²±´±µ±·±¸±¹±Á±Ê±Ï±Ð±Ò±Õ±ß±à±á±ä±ç±è±ê±î±ð±ñ±ô±õ±ö±÷±ý²¢²¦²§²¬²µ²·²¹²Æ²Î²Ï²Ð²Ñ²Ò²Ó²Ô²Õ²Ö²×²Þ²à²á²â²ã²ï²ó²ô²õ²ö²÷²ø²ù²ú²û²ü³¡³¢³¤³¥³¦³§³©³®³µ³¹³¾³Á³Â³Ä³Å³Æ³Í³Ï³Ò³Õ³Ù³Û³Ü³Ý³ã³å³æ³è³ë³ì³ï³ñ³ó³÷³ø³ú³û´¡´¢´¥´¦´«´¯´³´´´¸´¿´Â´Ç´Ê´Í´Ï´Ð´Ñ´Ó´Ô´Õ´Ú´Ü´í´ï´ø´ûµ£µ¥µ¦µ§µ¨µ¬µ®µ¯µ±µ²µ³µ´µµµ·µºµ»µ¼µÁµÆµËµÐµÓµÝµÞµßµãµæµçµíµòµöµ÷µüµýµþ¶¤¶¥¶§¶©¶ª¶«¶¯¶°¶³¶·¶¿¶À¶Á¶Ä¶Æ¶Í¶Ï¶Ð¶Ò¶Ó¶Ô¶Ö¶Ù¶Û¶á¶é¶ì¶î¶ï¶ñ¶ö¶ù¶û¶ü·¡·¢·£·§·©·¯·°·³·¶···¹·Ã·Ä·É·Ì·Ï·Ñ·×·Ø·Ü·ß·à·á·ã·æ·ç·è·ë·ì·í·ï·ô·ø¸§¸¨¸³¸´¸º¸¼¸¾¸¿¸Ã¸Æ¸Ç¸É¸Ë¸Ï¸Ñ¸Ó¸Ô¸Õ¸Ö¸Ù¸Ú¸Þ¸ä¸é¸ë¸ó¸õ¸ö¸ø¹¨¹¬¹®¹±¹³¹µ¹¶¹¹¹º¹»¹Æ¹Ë¹Ð¹Ò¹Ø¹Û¹Ý¹ß¹á¹ã¹æ¹è¹é¹ê¹ë¹ì¹î¹ñ¹ó¹ô¹õ¹ö¹ø¹ú¹ýº§º«ºººÅºÒº×ºØºáºäºèºìºóºø»¤»¦»§»©»ª»­»®»°»³»µ»¶»·»¹»º»»»½»¾»À»Á»Æ»Ñ»Ó»Ô»Ù»ß»à»á»â»ã»ä»å»æ»ç»ë»ï»ñ»õ»ö»÷»ú»ý¼¢¼£¼¥¼¦¼¨¼©¼«¼­¼¶¼·¼¸¼»¼Á¼Ã¼Æ¼Ç¼Ê¼Ì¼Í¼Ð¼Ô¼Õ¼Ö¼Ø¼Û¼Ý¼ß¼à¼á¼ã¼ä¼è¼ê¼ë¼ì¼î¼ï¼ð¼ñ¼ò¼ó¼õ¼ö¼÷¼ø¼ù¼ú¼û¼ü½¢½£½¤½¥½¦½§½«½¬½¯½°½±½²½´½º½½½¾½¿½Á½Â½Ã½Ä½Å½È½É½Ê½Î½Ï½Õ½×½Ú¾¥¾¨¾ª¾­¾±¾²¾µ¾¶¾·¾º¾»¾À¾Ç¾É¾Ô¾Ù¾Ý¾â¾å¾ç¾é¾î½Ü½à½á½ë½ì½ô½õ½ö½÷½ø½ú½ý¾¡¾¢¾£¾õ¾ö¾÷¾ø¾û¾ü¿¥¿ª¿­¿Å¿Ç¿Î¿Ñ¿Ò¿Ù¿â¿ã¿ä¿é¿ë¿í¿ó¿õ¿ö¿÷¿ù¿úÀ¡À£À©À«À¯À°À³À´ÀµÀ¶À¸À¹ÀºÀ»À¼À½À¾À¿ÀÀÀÁÀÂÀÃÀÄÀÅÀÌÀÍÀÔÀÖÀØÀÝÀàÀáÀéÀêÀëÀïÀðÀñÀöÀ÷ÀøÀùÀúÁ¤Á¥Á©ÁªÁ«Á¬Á­Á¯Á°Á±Á²Á³Á´ÁµÁ¶Á·Á¸Á¹Á½Á¾ÁÂÁÆÁÉÁÍÁÔÁÙÁÚÁÛÁÝÁÞÁäÁåÁèÁéÁëÁìÁóÁõÁúÁûÁüÁýÂ¢Â£Â¤Â¥Â¦Â§Â¨Â«Â¬Â­Â®Â¯Â°Â±Â²Â³Â¸Â»Â¼Â½Â¿ÂÀÂÁÂÂÂÅÂÆÂÇÂËÂÌÂÍÂÎÂÏÂÐÂÒÂÕÂÖÂ×ÂØÂÙÂÚÂÛÂÜÂÞÂßÂàÂáÂâÂæÂçÂèÂêÂëÂìÂíÂîÂðÂòÂóÂôÂõÂöÂ÷ÂøÂùÂúÃ¡Ã¨ÃªÃ­Ã³Ã´Ã¹Ã»Ã¾ÃÅÃÆÃÇÃÌÃÎÃÐÃÕÃÖÃÙÃÝÃàÃåÃíÃðÃõÃöÃùÃúÃýÄ±Ä¶ÄÅÄÆÄÉÄÑÄÓÄÔÄÕÄÖÄÙÄÚÄâÄåÄìÄíÄðÄñÄôÄöÄ÷ÄøÄûÄüÄþÅ¡Å¢Å¥Å¦Å§Å¨Å©Å±ÅµÅ·Å¸Å¹Å»Å½ÅÌÅÓÅ×ÅâÅçÅôÆ­Æ®ÆµÆ¶Æ»Æ¾ÆÀÆÃÆÄÆËÆÌÆÓÆ×ÆÜÆàÆêÆëÆïÆñÆôÆøÆúÆýÇ£Ç¤Ç¥Ç¦Ç¨Ç©Ç«Ç®Ç¯Ç±Ç³Ç´ÇµÇ¹ÇºÇ½Ç¾Ç¿ÇÀÇÂÇÅÇÇÇÈÇÌÇÏÇÔÇÕÇ×ÇÞÇáÇâÇãÇêÇëÇìÇíÇîÇ÷ÇøÇûÇýÈ£È§È¨È°È´ÈµÈ·ÈÃÈÄÈÅÈÆÈÈÈÍÈÏÈÒÈÙÈÞÈíÈñÈòÈóÈ÷ÈøÈúÈüÈþÉ¡É¥É§É¨É¬É±É²É´É¸É¹É¾ÉÁÉÂÉÄÉÉÉÊÉËÉÍÉÕÉÜÉÞÉãÉåÉèÉðÉóÉôÉöÉøÉùÉþÊ¤Ê¥Ê¦Ê¨ÊªÊ«Ê¬Ê±Ê´ÊµÊ¶Ê»ÊÆÊÊÊÍÊÎÊÓÊÔÊÙÊÞÊàÊäÊéÊêÊôÊõÊ÷ÊúÊýË§Ë«Ë­Ë°Ë³ËµË¶Ë¸Ë¿ËÇËÊËËËÌËÏËÐËÓËÕËßËàËäËæËçËêËïËðËñËõËöËøÌ¡Ì¢Ì§Ì¨Ì¬Ì¯Ì°Ì±Ì²Ì³Ì·Ì¸Ì¾ÌÀÌÌÌÎÌÐÌÖÌÚÌÜÌàÌâÌåÌëÌõÌùÌúÌüÌýÌþÍ­Í³Í·ÍºÍ¼Í¿ÍÅÍÇÍÉÍÑÍÒÍÔÍÕÍÖÍÝÍàÍäÍåÍçÍòÍøÎ¤Î¥Î§ÎªÎ«Î¬Î­Î°Î±Î³Î¹Î½ÎÀÎÂÎÅÎÆÎÈÎÊÎÍÎÎÎÏÎÐÎÑÎÔÎØÎÙÎÚÎÛÎÜÎÞÎßÎâÎëÎíÎñÎóÎýÎþÏ®Ï°Ï³Ï·Ï¸ÏºÏ½Ï¿ÏÀÏÁÏÃÏÅÏÇÏÊÏËÏÌÏÍÏÎÏÐÏÔÏÕÏÖÏ×ÏØÏÚÏÛÏÜÏßÏáÏâÏçÏêÏìÏîÏôÏùÏúÏþÐ¥Ð«Ð­Ð®Ð¯Ð²Ð³Ð´ÐºÐ»Ð¿ÐÆÐËÐ×ÐÚÐâÐåÐéÐêÐëÐíÐðÐ÷ÐøÐùÐüÑ¡Ñ¢Ñ¤Ñ§Ñ«Ñ¯Ñ°Ñ±ÑµÑ¶Ñ·Ñ¹Ñ»Ñ¼ÑÆÑÇÑÈÑËÑÌÑÎÑÏÑÒÑÕÑÖÑÞÑáÑâÑåÑèÑéÑìÑîÑïÑñÑôÑ÷ÑøÑùÑþÒ¡Ò¢Ò£Ò¤Ò¥Ò©Ò¯Ò³ÒµÒ¶Ò½Ò¿ÒÃÒÅÒÇÒÍÒÏÒÕÒÚÒäÒåÒèÒéÒêÒëÒìÒïÒñÒõÒøÒûÒþÓ£Ó¤Ó¥Ó¦Ó§Ó¨Ó©ÓªÓ«Ó¬Ó®Ó±Ó´ÓµÓ¶Ó¸Ó»Ó½Ó¿ÓÅÓÇÓÊÓËÓÌÓÎÓÕÓÚÓßÓãÓæÓéÓëÓìÓïÓõÓùÓüÓþÔ¤Ô¦Ô§Ô¨Ô¯Ô°Ô±Ô²ÔµÔ¶Ô¸Ô¼Ô¾Ô¿ÔÀÔÁÔÃÔÄÔÆÔÇÔÈÔÉÔËÔÌÔÍÔÎÔÏÔÓÔÖÔØÔÜÔÝÔÞÔßÔàÔäÔæÔîÔðÔñÔòÔóÔôÔùÔúÔýÔþÕ¡Õ¢Õ¤Õ©Õ«Õ®Õ±ÕµÕ¶Õ·Õ¸Õ»Õ½ÕÀÕÅÕÇÕÊÕËÕÍÕÔÕÝÕÞÕàÕâÕêÕëÕìÕïÕòÕóÕõÕöÕøÕùÖ¡Ö¢Ö£Ö¤Ö¯Ö°Ö´Ö½Ö¾Ö¿ÖÀÖÄÖÆÖÊÖÍÖÓÖÕÖÖÖ×ÖÚÖßÖáÖåÖçÖèÖíÖîÖïÖòÖõÖöÖüÖýÖþ×¢×¤×¨×©×ª×¬×®×¯×°×±×³×´×¶×¸×¹×º×»×¼×Å×Ç×È×Ê×Õ×Ù×Û×Ü×Ý×Þ×ç×é×êï¹àÈæÈè¨êÓö°ÚÏï§ðÆæÁæñ÷¡îÙßÂîÓð±öµðÇêÚï¼ÜêßÙääîéóÙõÏÜÐçÂóÖæôì©ì­ïÚïð÷§ÙÏçÍéÄéëë÷ïÙ÷Æ÷ÞÙ÷âÄîàð¾îßæî÷õâüïÊÙ­îÎÙæÚÆÚßÝÛâãæ¿æöêèìøïâØöÜÉâêãÑöðíºØ÷ÚÈé´í×ö³èÇèßîñîõâÁð·ï¥Ù±àüöÅÛ»ç©õéîËâëç¶ðÈê¡öºðËÜÊæõèÈê£ß¥ï±õºßÕ÷²ææçªééêæð÷óìÚÔí¸ñÉìâïëÙáÚ®ÚÐç°êëïááÛîäñ²ï¢öôöøîúîûá´ð´ñ¼äÂèüë¹óÆ÷òóýí¡ïæìÀõ»îìÚÌÛÑãÕéîï°ïÉðÊò¦ò§öùÚÀåÇîïð¹öÜîÕöÐç³ïÐöîÙÇããÙìæâç¦ç¨êçôïöÖöûîÅêàÞÏß¦ç¤í°ØºÚ¾çÉï¯æüïÓò£Ø¨âÙç®öáÚ¸çÃêíÚ¬ì±îÜïÀð³ðÀ÷½ð»ÞâÚ´ÞèðÙ÷¤áîØÐØÛæ£èíöÙ÷¬ÙòçµöçÛößÃàþé¤òåîþãÛç¬ò¡å°ò«Ú­ãØòÃÙäÚ§Ý¦ãÈö×ä°ðÉæèèëîüÛ¼çÙïÌöéöüÚ¶ÜößÜä«çÀçõêÍÚ»âÆãÔîØïìÚ¦ÚµÜùß´ßâæ÷çáêéì´í¶î¿ò²õÒö«öÝöêÛ£ä¤îòïØòÍÚÉçÌê§ê¯íúðÏóÈöä÷µç­çÖÞØá½ðÔöÞðÜò¢öÚÚáÝ£âËçÆêáêîØÙãþåÉåòëÖö¦ãÎð¯ðÕÚªåðé·ì«îÒï¸ñÀö´ïÃïÔöÁÚÜçåñäØÜÛîâéâýîøïÇíèãÊîÖîíæìç¼éðîÝï¾ò¥ö¸ï¬à·Û¦ßàëÚáö÷ÅÚ²Ú¿Ú÷ÛÛæþêÜØÑÝÞã´ñùóñãÍï¿öïòÓáÁáâäµäþêãíùïªñ®ô¥á°é­ìµïçñÜãÏï¶ßëáÀîîï©ðì÷¦Ú³çÐÙ³ÛªÛÞÜÂÝ°Ýñß¿åÎæêçÊèÀèÝéöíÂï®ð¿ðÝôÏõÈö¨öâ÷¯ÝüÞÆäòçöéçñÍñÏöã÷ËçÔîÉðÓÝþâÞéÝê¥õïç±èùòÉöìä¯æòç¸ïÖðÒÜ×ãñççèÐëÊíÃÙÍÝäà¶áÐïÎðüñïò÷÷ÃÛäß£ààãÌãòäËèÓéÖéñéûê¤ëªëÍðµðØôµöÔÙõæ®èïð½öÇàðÜýâ¤ãøé¡ëáïÝéµñÚï²ß¼ßéæÖè¿Û½çÏïÜòª÷©÷áÞÑìËí¯îÍØÂÚ×â¨ìòäÅëïö¼ç¿çÑãÉçÅÚÓÝëâÉéâïÒîâîóÚ«îêöòéýöóÜàôÁÚíÞÁà¿ò©õæÜÑßÌñ÷Ù¯ßææåîÏÙÐÚ©âæê±õçðåàÎç¢î¼îëÚÒæéçÎæÉîÇïäïèÞ­æëç²èçíÓñýñþ÷¢ÙÝÝ¡ã¥å¹ç×èýîÔæÍéÉê¨ìÁïºïÏïêôÇõÄÚ½ÚÛÜñçØíÍõÎã«ïÆóæï·ÞìöëÜäòÌÛÏêäò±öúÚ°á«ãÖêïð¶Ú¹ç¹éúîýã×ãÚí¨Üéæ¬èãâ¿éíáÉòîçÈï¨ò­ò¹ìªë§ôÖçÒØÄï¤ð£ï¡öèõ§Ú¨æ©æóîÌ÷­ÛðéäõüØÇäÜî´Ú·ÚÅäÉÚÖÛõÝªß±éøêÛîæöåç·Þóç£ãÅîåØËæáçÁïÈð¸Þ´âÈì¬ïËÚÕöÕÚÇÝ¥áøßïíüãËîè÷£îÑöØê¼îãïÄñüÙÎâ¼ï¦ïÛèºï«ç¾ðÃãÙôÐö¶öæâúî×îÊÞÒâ½óêö¾æ´ëðæýçºéþÚÃàøãÇãíä¶çâè¸ì¿öÛãÓÝ«ö»ÚùâÐâäåüæððÄðÍâ¾ãÒçôêêíÌÜÈÝ²Þºá­áýæµðÂðïòºôÌõÑÜ¼âÃæøç½÷ÏßØäìæçç¯èÉóïÙôß¢ç¥çÓÚêÜþâÊð¼Ú¼çïÚÎîçïàÚÊí´÷¨Û÷ä±öàÛëæ«èâë²ØÉØÍÙ²ÙðÚÝâûãÆõ¦÷Ê÷Ð÷úì¾é÷ðÎ÷¥ØÌÚËÚþêÊìÇÚ±ß½á»âÂâøæäçËéóêÝîÆï×ïîðùô¯î÷ñ«ÜãÝºÝÓÝöÞüàÓäÞäëè¬ðÐñ¨ò¤ó¿ïÞÝµîðöÏØñÙ¶ÚÄÚÍÝ÷áÎâÀãÐåýæúêìì£îÚðÁðÖö¹éÚð°ö½îáÛ©Ü¿ã¢ã³ç¡è¹éæëµè¶ôõöÉæàØÓßõàýóåÚÚçÕÚÞÚ¯îÈÚØéüðÑä¥çÇèåéôêâìõð²Úºá¿îÛï£óÝæïèÎèÙéòéùêÞðºòÏôêõÙõÜö£ïñæûç§ØùéÆîùßùâÍò¨æíçÄÚÂïíÚÑç»ê¢êßíöïÅö·ööÙÌÚÁæãöíïßçÚõò÷®Ú¥ÚÙÛ§ÛÂÛÊÛàÛâÛñÛûÛþÜÜÝ¤Ý§Ý¯Ý»ÝÔÞ»Þêß¸ßÄßÇßÐßÔàÙàèàëá¥á®áÕáÝáèáïáóâÅâÇâÌâÎã¶ãÀãÁãÜäÓäÙäãäíäóå£å¸æùç«ç´çëèÅèðèñéÀéÍéïéõêåëÉëËì®ìÎìÑìÖíªíµí¿íÞíîîÐîÞîôîöï­ï³ï´ïµï»ï½ïÁïÂïÍïÑïÕïãïåïéïïïùðÅðÌð×ðßðâðéñ³ñÐñßñìò¬òýôðöÑöÒöÓößöñöõö÷öýöþ÷ª÷«÷³÷¹÷þ";

    }
}