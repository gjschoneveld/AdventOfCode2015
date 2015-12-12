﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day5
{
    class Program
    {
        static bool IsNice1(string x)
        {
            // three vowels
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            bool threeVowels = x.Count(c => vowels.Contains(c)) >= 3;

            // letter twice in a row
            bool letterTwice = false;
            for (int i = 1; i < x.Length; i++)
			{
			    if (x[i] == x[i - 1]) 
                {
                    letterTwice = true;
                    break;
                }
			}

            // forbidden strings
            bool forbidden = false;
            string[] forbiddenSegments = { "ab", "cd", "pq", "xy" };
            foreach (var segment in forbiddenSegments)
        	{
                if (x.Contains(segment)) 
                {
                    forbidden = true;
                    break;
                }
        	}

            return threeVowels && letterTwice && !forbidden;
        }

        static bool IsNice2(string x)
        {
            // double pairs without overlap like abcab (ab is double pair)
            bool doublePair = false;

            List<string> allPairs = new List<string>();
            for (int i = 0; i < x.Length - 1; i++)
            {
                string pair = new string(new char[] { x[i], x[i + 1] });
                allPairs.Add(pair);
            }

            Dictionary<string, int> pairPositions = new Dictionary<string, int>();
            for (int i = 0; i < allPairs.Count; i++)
            {
                var pair = allPairs[i];

                if (pairPositions.ContainsKey(pair))
                {
                    // only found if no overlap
                    if (i - pairPositions[pair] > 1)
                    {
                        doublePair = true;
                        break;
                    }
                }
                else
                {
                    pairPositions.Add(pair, i);
                }
            }

            // letter repeating
            bool letterRepeating = false;
            for (int i = 2; i < x.Length; i++)
			{
			    if (x[i] == x[i - 2]) 
                {
                    letterRepeating = true;
                    break;
                }
			}

            return doublePair && letterRepeating;
        }

        static void Main(string[] args)
        {
            string input = "uxcplgxnkwbdwhrp\nsuerykeptdsutidb\ndmrtgdkaimrrwmej\nztxhjwllrckhakut\ngdnzurjbbwmgayrg\ngjdzbtrcxwprtery\nfbuqqaatackrvemm\npcjhsshoveaodyko\nlrpprussbesniilv\nmmsebhtqqjiqrusd\nvumllmrrdjgktmnb\nptsqjcfbmgwdywgi\nmmppavyjgcfebgpl\nzexyxksqrqyonhui\nnpulalteaztqqnrl\nmscqpccetkktaknl\nydssjjlfejdxrztr\njdygsbqimbxljuue\nortsthjkmlonvgci\njfjhsbxeorhgmstc\nvdrqdpojfuubjbbg\nxxxddetvrlpzsfpq\nzpjxvrmaorjpwegy\nlaxrlkntrukjcswz\npbqoungonelthcke\nniexeyzvrtrlgfzw\nzuetendekblknqng\nlyazavyoweyuvfye\ntegbldtkagfwlerf\nxckozymymezzarpy\nehydpjavmncegzfn\njlnespnckgwmkkry\nbfyetscttekoodio\nbnokwopzvsozsbmj\nqpqjhzdbuhrxsipy\nvveroinquypehnnk\nykjtxscefztrmnen\nvxlbxagsmsuuchod\npunnnfyyufkpqilx\nzibnnszmrmtissww\ncxoaaphylmlyljjz\nzpcmkcftuuesvsqw\nwcqeqynmbbarahtz\nkspontxsclmbkequ\njeomqzucrjxtypwl\nixynwoxupzybroij\nionndmdwpofvjnnq\ntycxecjvaxyovrvu\nuxdapggxzmbwrity\ncsskdqivjcdsnhpe\notflgdbzevmzkxzx\nverykrivwbrmocta\nccbdeemfnmtputjw\nsuyuuthfhlysdmhr\naigzoaozaginuxcm\nycxfnrjnrcubbmzs\nfgbqhrypnrpiizyy\ntaoxrnwdhsehywze\nechfzdbnphlwjlew\njhmomnrbfaawicda\nfywndkvhbzxxaihx\naftuyacfkdzzzpem\nyytzxsvwztlcljvb\niblbjiotoabgnvld\nkvpwzvwrsmvtdxcx\nardgckwkftcefunk\noqtivsqhcgrcmbbd\nwkaieqxdoajyvaso\nrkemicdsrtxsydvl\nsobljmgiahyqbirc\npbhvtrxajxisuivj\nggqywcbfckburdrr\ngmegczjawxtsywwq\nkgjhlwyonwhojyvq\nbpqlmxtarjthtjpn\npxfnnuyacdxyfclr\nisdbibbtrqdfuopn\nvucsgcviofwtdjcg\nywehopujowckggkg\nmzogxlhldvxytsgl\nmllyabngqmzfcubp\nuwvmejelibobdbug\nbrebtoppnwawcmxa\nfcftkhghbnznafie\nsqiizvgijmddvxxz\nqzvvjaonnxszeuar\nabekxzbqttczywvy\nbkldqqioyhrgzgjs\nlilslxsibyunueff\nktxxltqgfrnscxnx\niwdqtlipxoubonrg\ntwncehkxkhouoctj\nbdwlmbahtqtkduxz\nsmbzkuoikcyiulxq\nbjmsdkqcmnidxjsr\nicbrswapzdlzdanh\neyszxnhbjziiplgn\npdxhrkcbhzqditwb\nnfulnpvtzimbzsze\nglayzfymwffmlwhk\nbejxesxdnwdlpeup\nukssntwuqvhmsgwj\nhoccqxlxuuoomwyc\nrapztrdfxrosxcig\ncxowzhgmzerttdfq\nyzhcurqhdxhmolak\nkqgulndpxbwxesxi\nyjkgcvtytkitvxiu\nxnhfqhnnaceaqyue\nqkuqreghngfndifr\nxesxgeaucmhswnex\noccbvembjeuthryi\ndmefxmxqjncirdwj\nystmvxklmcdlsvin\npplykqlxmkdrmydq\ncbbjkpbdvjhkxnuc\nembhffzsciklnxrz\nasrsxtvsdnuhcnco\nxcbcrtcnzqedktpi\nmglwujflcnixbkvn\nmnurwhkzynhahbjp\ncekjbablkjehixtj\nkbkcmjhhipcjcwru\nusifwcsfknoviasj\nrsfgocseyeflqhku\nprgcyqrickecxlhm\nasbawplieizkavmq\nsylnsirtrxgrcono\nnzspjfovbtfkloya\nqfxmsprfytvaxgtr\nyckpentqodgzngnv\nycsfscegcexcnbwq\nkbmltycafudieyuh\ntpahmvkftilypxuf\nqivqozjrmguypuxu\ngdhbfradjuidunbk\nvxqevjncsqqnhmkl\nrpricegggcfeihst\nxucvzpprwtdpzifq\negyjcyyrrdnyhxoo\nkfbrzmbtrrwyeofp\nqpjdsocrtwzpjdkd\nreboldkprsgmmbit\nvwkrzqvvhqkensuy\nydvmssepskzzvfdp\nvqbigplejygdijuu\nmzpgnahrhxgjriqm\nuiejixjadpfsxqcv\ntosatnvnfjkqiaha\nyipuojpxfqnltclx\npcxwvgcghfpptjlf\nshrudjvvapohziaj\njdckfjdtjsszdzhj\nhgisfhcbdgvxuilk\ngytnfjmrfujnmnpp\nohflkgffnxmpwrrs\njzxajbkwwjknasjh\nxrcxfollmejrislv\ndjjlwykouhyfukob\nrittommltkbtsequ\nlpbvkxdcnlikwcxm\nvkcrjmcifhwgfpdj\ndkhjqwtggdrmcslq\nswnohthfvjvoasvt\nyrzoksmcnsagatii\nduommjnueqmdxftp\ninlvzlppdlgfmvmx\nxibilzssabuqihtq\ninkmwnvrkootrged\nldfianvyugqtemax\ngbvwtiexcuvtngti\ntemjkvgnwxrhdidc\naskbbywyyykerghp\nonezejkuwmrqdkfr\nkybekxtgartuurbq\nubzjotlasrewbbkl\nstueymlsovqgmwkh\nlhduseycrewwponi\nyohdmucunrgemqcu\nonnfbxcuhbuifbyc\nodrjkigbrsojlqbt\nimqkqqlkgmttpxtx\nsxmlkspqoluidnxw\nakaauujpxhnccleb\nxvgpghhdtpgvefnk\njdxeqxzsbqtvgvcq\nmdusenpygmerxnni\nagihtqvgkmgcbtaw\ndovxcywlyvspixad\nuulgazeyvgtxqkfz\nndhmvrwuflhktzyo\nhcaqkmrbvozaanvm\ntvfozbqavqxdqwqv\nrlkpycdzopitfbsv\ndmyjtmjbtnvnedhs\nfmwmqeigbzrxjvdu\ntwgookcelrjmczqi\ngrxosmxvzgymjdtz\nzsstljhzugqybueo\njpeapxlytnycekbd\niasykpefrwxrlvxl\nazohkkqybcnsddus\naoaekngakjsgsonx\nawsqaoswqejanotc\nsgdxmketnjmjxxcp\nylnyuloaukdrhwuy\newoqjmakifbefdib\nytjfubnexoxuevbp\newlreawvddptezdd\nvmkonztwnfgssdog\nahbpuqygcwmudyxn\nkmahpxfjximorkrh\notjbexwssgpnpccn\naewskyipyztvskkl\nurqmlaiqyfqpizje\nnrfrbedthzymfgfa\nvndwwrjrwzoltfgi\niiewevdzbortcwwe\nqiblninjkrkhzxgi\nxmvaxqruyzesifuu\nyewuzizdaucycsko\nhmasezegrhycbucy\ndwpjrmkhsmnecill\nhnffpbodtxprlhss\navmrgrwahpsvzuhm\nnksvvaswujiukzxk\nzzzapwhtffilxphu\nvwegwyjkbzsrtnol\nqurpszehmkfqwaok\niknoqtovqowthpno\nbrlmpjviuiagymek\nefxebhputzeulthq\nmzkquarxlhlvvost\nxsigcagzqbhwwgps\nqufztljyzjxgahdp\ndlfkavnhobssfxvx\nhgdpcgqxjegnhjlr\nfboomzcvvqudjfbi\nwnjuuiivaxynqhrd\nnhcgzmpujgwisguw\nwjeiacxuymuhykgk\nqmeebvxijcgdlzpf\nnmmnxsehhgsgoich\nejluaraxythbqfkl\nmdbsbwnaypvlatcj\nnnfshfibmvfqrbka\ndvckdmihzamgqpxr\nfoztgqrjbwyxvewk\nokpryqcbvorcxhoh\nfpiwsndulvtthctx\nzrbiovlmzdmibsiq\nsetwafbnnzcftutg\nnyvqghxhgkxfobdm\nenpvqadzarauhajl\ntwblhpvkazpdmhmr\nlbhlllsgswvhdesh\ntdfwkgxnqjxcvsuo\nlnvyjjbwycjbvrrb\njsxqdvmzaydbwekg\nxirbcbvwlcptuvoa\nhwnukxenilatlfsk\nkhwopjqkxprgopmd\nsljzdoviweameskw\nstkrdmxmpaijximn\nfdilorryzhmeqwkc\nmfchaaialgvoozra\ngjxhoxeqgkbknmze\nbeowovcoqnginrno\nmkgmsgwkwhizunxo\nphnhfusyoylvjdou\ncsehdlcmwepcpzmq\npgojomirzntgzohj\nfkffgyfsvwqhmboz\nmrvduasiytbzfwdn\nepzrmsifpmfaewng\nooqxnoyqrlozbbyf\nahcxfmgtedywrbnx\nibqktvqmgnirqjot\nxarssauvofdiaefn\nxradvurskwbfzrnw\nnxklmulddqcmewad\ntwichytatzoggchg\nqmgvroqwrjgcycyv\nyvezgulgrtgvyjjm\njgmcklzjdmznmuqk\nbytajdwwconasjzt\napjttucpycyghqhu\nflfejjzihodwtyup\ngmrtrwyewucyqotv\nnlohdrlymbkoenyl\nwxcmqwbrwgtmkyfe\nnjtzlceyevmisxfn\nhtbbidsfbbshmzlt\ngxhjeypjwghnrbsf\ncifcwnbtazronikv\nezvjijcjcyszwdjy\nsrffeyrvyetbecmc\nxpjefrtatrlkbkzl\nyhncvfqjcyhsxhbb\npqhcufzlcezhihpr\nqtdsfvxfqmsnzisp\ndfonzdicxxhzxkrx\nmqqqzhxkyfpofzty\ndodjadoqyxsuazxt\njjwkrlquazzjbvlm\nttosfloajukoytfb\nllateudmzxrzbqph\ncriqihrysgesmpsx\nnpszvlittbcxxknj\nqmzojrvraitrktil\ncfyoozzpwxwkwoto\ndaxohtcgvtktggfw\nvthkpkoxmiuotjaj\npkfkyobvzjeecnui\nojcjiqrfltbhcdze\nscbivhpvjkjbauun\nysowvwtzmqpjfwyp\nlaeplxlunwkfeaou\njufhcikovykwjhsa\nxrucychehzksoitr\npyaulaltjkktlfkq\noypfrblfdhwvqxcv\nzybrgxixvhchgzcf\npuoagefcmlxelvlp\nxjnhfdrsbhszfsso\nocgvzryoydaoracw\nbxpnqllmptkpeena\npziyeihxlxbbgdio\nbvtrhtlbfzmglsfc\nggpuvtseebylsrfk\npukenexjqecnivfj\njswabfbzpnhhdbpn\nenojrtwqpfziyqsv\nrjtmxudgcudefuiz\niqmjxynvtvdacffc\nuheywxlsusklitvl\nkwhxduejafdpmqdc\nrspgblenbqlmcltn\nrczhurnrqqgjutox\ndqhytibjzxkdblzl\nhpbieadydiycvfys\npucztfoqvenxiuym\nnqpfzgpblwijiprf\nltgseeblgajbvltk\nmwxukbsnapewhfrc\ndvxluiflicdtnxix\npexfbpgnqiqymxcq\ndakudfjjwtpxuzxy\nletlceyzlgmnrewu\nojktahbsdifdfhmd\nanezoybbghjudbih\nsawxtlvzysaqkbbf\nttnkctcevpjiwqua\nedrwrdvbaoqraejd\nwnbfilvuienjxlcr\nwqhzwvyybyxhhtsm\njxbgvyaqczwdlxfo\nwbypqfmbwrsvfmdv\nizdxjyfpidehbets\nvbxbggqseurknjor\negpmpoxickhvwdlz\nivfrzklvpwoemxsy\nxkziseheibmrpdww\nxnrmtoihaudozksa\nefemdmbxdsaymlrw\nyjdjeckmsrckaagx\nvlftqxxcburxnohv\nfwyquwgajaxebduj\ndwpmqvcxqwwnfkkr\nisduxxjfsluuvwga\navdtdppodpntojgf\nvrcoekdnutbnlgqk\nkbhboxjmgomizxkl\ncgsfpjrmewexgzfy\nusdtnhjxbvtnafvp\nbjoddgxbuxzhnsqd\nhoyqdzofddedevsb\nrwiwbvqfjajotaoj\niabomphsuyfptoos\nbubeonwbukprpvhy\nxurgunofmluhisxm\npuyojzdvhktawkua\ndbvqhztzdsncrxkb\noaeclqzyshuuryvm\nnmgwfssnflxvcupr\nvjkiwbpunkahtsrw\nromyflhrarxchmyo\nyecssfmetezchwjc\nqwtocacqdslhozkd\nmesexvfbtypblmam\nmtjucgtjesjppdtt\npvodhqqoeecjsvwi\nvvlcwignechiqvxj\nwiqmzmmjgjajwgov\nkwneobiiaixhclev\nlkdeglzrrxuomsyt\noqovuwcpwbghurva\nlfsdcxsasmuarwwg\nawkbafhswnfbhvck\nsztxlnmyvqsiwljg\nhozxgyxbcxjzedvs\noifkqgfqmflxvyzn\nmfvnehsajlofepib\ndelgbyfhsyhmyrfa\nuenimmwriihxoydv\nvjqutpilsztquutn\nkfebsaixycrodhvl\ncoifyqfwzlovrpaj\nxiyvdxtkqhcqfsqr\nhoidcbzsauirpkyt\nfiumhfaazfkbaglq\nfzwdormfbtkdjgfm\nfaxqrortjdeihjfv\nljhaszjklhkjvrfi\npzrxsffkuockoqyl\nimmbtokjmwyrktzn\nlzgjhyiywwnuxpfx\nvhkocmwzkfwjuzog\nghntjkszahmdzfbl\ngbcthxesvqbmzggy\noyttamhpquflojkh\nnbscpfjwzylkfbtv\nwnumxzqbltvxtbzs\njfhobjxionolnouc\nnrtxxmvqjhasigvm\nhweodfomsnlgaxnj\nlfgehftptlfyvvaj\nccoueqkocrdgwlvy\neuhgvirhsaotuhgf\npdlsanvgitjvedhd\nseokvlbhrfhswanv\npntdqaturewqczti\njkktayepxcifyurj\ndhzzbiaisozqhown\nwehtwakcmqwczpbu\nzwvozvspqmuckkcd\nefucjlrwxuhmjubr\nlzodaxuyntrnxwvp\nqdezfvpyowfpmtwd\nmizijorwrkanesva\ntxmitbiqoiryxhpz\nxhsqgobpouwnlvps\nmuixgprsknlqaele\ndisgutskxwplodra\nbmztllsugzsqefrm\nymwznyowpaaefkhm\nebfifzloswvoagqh\npkldomvvklefcicw\nziqzbbfunmcgrbtq\niuekfpbkraiwqkic\njflgjidirjapcuqo\nachsfbroyrnqnecg\nudbhouhlgjjzapzr\narerrohyhhkmwhyo\ntxyjzkqexgvzdtow\nogzrjwibvzoucrpg\nrfdftaesxdnghwhd\naxdhwmpuxelmpabo\ngtktemowbsvognac\nwkfuclilhqjzxztk\nqbwjouutzegaxhrz\nopfziwqqbwhzzqhj\npvcvcsupfwsmeacs\nxsbohvbguzsgpawn\nsczoefukwywxriwj\noqkhcqfdeaifbqoc\nvtsrholxbjkhwoln\nyuvapljnwbssfbhi\ndxdfwccqvyzeszyl\ngdbmjtonbiugitmb\nqunirtqbubxalmxr\nzzxsirhdaippnopr\nfibtndkqjfechbmq\ngqgqyjvqmfiwiyio\nihwsfkwhtzuydlzw\neygyuffeyrbbhlit\nzdlsaweqomzrhdyy\nptbgfzuvxiuuxyds\nllxlfdquvovzuqva\nwfrltggyztqtyljv\nkwipfevnbralidbm\ngbhqfbrvuseellbx\nobkbuualrzrakknv\nhlradjrwyjgfqugu\nvtqlxbyiaiorzdsp\ntedcbqoxsmbfjeyy\ncxdppfvklbdayghy\ngjnofexywmdtgeft\nldzeimbbjmgpgeax\negrwsmshbvbawvja\nvadfrjvcrdlonrkg\nmojorplakzfmzvtp\njyurlsoxhubferpo\nijwqogivvzpbegkm\ncnmetoionfxlutzg\nlawigelyhegqtyil\nmqosapvnduocctcd\neqncubmywvxgpfld\nvigfretuzppxkrfy\nncwynsziydoflllq\ncbllqinsipfknabg\nndtbvdivzlnafziq\niqrrzgzntjquzlrs\ndamkuheynobqvusp\njxctymifsqilyoxa\nylritbpusymysmrf\npaoqcuihyooaghfu\nobhpkdaibwixeepl\nigrmhawvctyfjfhd\nybekishyztlahopt\nvkbniafnlfqhhsrq\nkltdigxmbhazrywf\nufhcoyvvxqzeixpr\nklcxdcoglwmeynjt\nfunpjuvfbzcgdhgs\nakgyvyfzcpmepiuc\nzhlkgvhmjhwrfmua\nibsowtbnrsnxexuz\nvpufbqilksypwlrn\nngrintxhusvdkfib\nziuwswlbrxcxqslw\nsucledgxruugrnic\nzwnsfsyotmlpinew\noaekskxfcwwuzkor\nqjmqwaktpzhwfldu\ntmgfgqgpxaryktxo\nqfaizepgauqxvffk\naddkqofusrstpamf\nshdnwnnderkemcts\ngwfygbsugzptvena\nfpziernelahopdsj\nbkkrqbsjvyjtqfax\ngxrljlqwxghbgjox\nipfwnqaskupkmevm\nnnyoyhnqyfydqpno\nlgzltbrrzeqqtydq\nfgzxqurhtdfucheb\njvpthtudlsoivdwj\nbmlhymalgvehvxys\nfhklibetnvghlgnp\nhfcyhptxzvblvlst\ndonanindroexgrha\noqawfmslbgjqimzx\njzgehjfjukizosep\nbhlgamcjqijpvipb\njrcrdjrvsyxzidsk\nouwfwwjqezkofqck\nwrvsbnkhyzayialf\nknhivfqjxrxnafdl\nhbxbgqsqwzijlngf\nqlffukpfmnxpfiyq\nevhxlouocemdkwgk\nbaxhdrmhaukpmatw\nnwlyytsvreqaminp\nljsjjzmlsilvxgal\nonunatwxfzwlmgpk\nnjgolfwndqnwdqde\nngdgcjzxupkzzbqi\nieawycvvmvftbikq\nccyvnexuvczvtrit\nenndfwjpwjyasjvv\ntcihprzwzftaioqu\nbkztdkbrxfvfeddu\nqkvhtltdrmryzdco\nrurtxgibkeaibofs\nmjxypgscrqiglzbp\nunpkojewduprmymd\ncsqtkhjxpbzbnqog\nmednhjgbwzlhmufi\nsfrwfazygygzirwd\nijqeupbrhhpqxota\ncmhpncanwudyysyh\nwwcxbwzrplfzrwxd\njriomldifuobjpmq\nradonyagpulnnyee\nryqjwxsspbbhnptd\nyeoqpnsdhludlmzf\nqsqlkeetyalenueh\nqnnedenwsjdrcrzt\nlejkuhsllxbhfcrx\nanddbvllrrqefvke\nwdtljquijaksvdsv\nadslgvfuqqdkzvbc\nwhbccefjpcnjwhaq\nkqrfuankaibohqsg\nfyxisfwihvylgnfd\nrwqdrddghyqudcif\nsyhzowthaaiiouaf\nzjmrtgrnohxmtidu\ndeecwkfvjffxrzge\ndztmvolqxkhdscxe\ncdghcrgavygojhqn\npepqmdbjhnbugqeu\npnumdjpnddbxhieg\njzfhxeyahiagizfw\nhdkwugrhcniueyor\ngmgudeqlbmqynflu\ntoidiotdmfkxbzvm\npyymuoevoezlfkjb\netrbwuafvteqynlr\nusvytbytsecnmqtd\ndfmlizboawrhmvim\nvrbtuxvzzefedlvs\nvslcwudvasvxbnje\nxdxyvoxaubtwjoif\nmduhzhascirittdf\ncqoqdhdxgvvvxamk\ndshnfwhqjbhuznqr\nzimthfxbdmkulkjg\nluylgfmmwbptyzpj\niujpcgogshhotqrc\ncaqcyzqcumfljvsp\nsprtitjlbfpygxya\nfnconnrtnigkpykt\nirmqaqzjexdtnaph\nbbqrtoblmltvwome\nozjkzjfgnkhafbye\nhwljjxpxziqbojlw\nzahvyqyoqnqjlieb\ndptshrgpbgusyqsc\nuzlbnrwetkbkjnlm\nyccaifzmvbvwxlcc\nwilnbebdshcrrnuu\nevxnoebteifbffuq\nkhbajekbyldddzfo\nkjivdcafcyvnkojr\nwtskbixasmakxxnv\nuzmivodqzqupqkwx\nrxexcbwhiywwwwnu\nrowcapqaxjzcxwqi\nfkeytjyipaxwcbqn\npyfbntonlrunkgvq\nqiijveatlnplaifi\nltnhlialynlafknw\nurrhfpxmpjwotvdn\nxklumhfyehnqssys\ncivrvydypynjdoap\nfvbmxnfogscbbnyd\noznavyflpzzucuvg\niyshrpypfbirahqo\nqmzbfgelvpxvqecy\nxkkxaufomsjbofmk\nirlouftdmpitwvlq\ncsjoptbdorqxhnjg\nbkryeshfsaqpdztm\nguxbdqzfafsjoadl\ntgrltexgrzatzwxf\ncwsgsijqdanubxad\nxafnexgturwrzyrg\napcrsqdbsbaxocxr\npspgxnzcevmvvejk\nszephmeegvegugdt\nndjsoloeacasxjap\nbdnfksliscnirjfu\nehglacmzpcgglpux\njwweijomqfcupvzw\nyesblmmkqhbazmdu\nsjsmalypmuslzgac\nfkiqatyttlnuhdho\ntlhnyuzdocvfdihq\nngehtjmycevnybga\nobxodzcdgtrycgry\nstkyrvdfbwovawmk\nbdkhqcfrqaxhxloo\ngpvumnuoiozipnrk\njbhanddinpqhxeol\nhwkzkmbmsrvunzit\nrfuomegkxbyamjpw\nyzbljuksletipzwm\neafedkagwitzqigl\nprenqvsbotqckgwy\nspedpbwzphdrfxfz\ncmsuqwemhwixkxet\nxgdyeqbqfldvaccq\neooxgsrfsbdaolja\nkyhqylxooewrhkho\nmswieugqpoefmspt\nuszoqundysdyeqlc\nhkmjdggxefdyykbq\ndtuhjnlaliodtlvh\noalbueqbhpxoxvvx\noowxtxsoqdwhzbya\nlclajfsrpmtwvzkm\nfxmjufpqtpyazeqo\nozlmreegxhfwwwmf\nmqzrajxtxbaemrho\nnfglecsyqduhakjr\nnkxqtmasjjkpkqbp\njjfonbqimybvzeus\nvjqkhkhjlmvpwkud\nwxxhnvfhetsamzjr\npladhajujzttgmsw\ndbycgxeymodsdlhm\nqxszeuaahuoxjvwu\nadultomodzrljxve\ndmhgrbhvvpxyzwdn\nslohrlwxerpahtyp\nmngbocwyqrsrrxdb\nfacyrtflgowfvfui\nhyvazpjucgghmmxh\ntwtrvjtncmewcxit\nuejkrpvilgccfpfr\npsqvolfagjfvqkum\nnvzolslmiyavugpp\nlpjfutvtwbddtqiu\nfkjnfcdorlugmcha\neaplrvdckbcqqvhq\nxrcydhkockycburw\niswmarpwcazimqxn\nkicnnkjdppitjwrl\nvwywaekzxtmeqrsu\ndxlgesstmqaxtjta\npmeljgpkykcbujbb\nvhpknqzhgnkyeosz\njprqitpjbxkqqzmz\nfiprxgsqdfymyzdl\ndzvfwvhfjqqsifga\naeakhfalplltmgui\nfrqrchzvenhozzsu\nhsvikeyewfhsdbmy\npuedjjhvxayiwgvg\nzmsonnclfovjoewb\nbnirelcaetdyaumi\nszvudroxhcitatvf\nsccfweuyadvrjpys\nyiouqrnjzsdwyhwa\nxyjhkqbnfmjjdefz\nfjwgemkfvettucvg\naapqpwapzyjnusnr\ndytxpkvgmapdamtc\nhgocpfoxlheqpumw\ntwzuiewwxwadkegg\nqdbosnhyqmyollqy\nfclbrlkowkzzitod\nsgxnrrpwhtkjdjth\nxckvsnkvnvupmirv\nnioicfeudrjzgoas\nlcemtyohztpurwtf\noyjxhhbswvzekiqn\nidkblbyjrohxybob\nrthvloudwmktwlwh\noyzhmirzrnoytaty\nysdfhuyenpktwtks\nwxfisawdtbpsmwli\nvgmypwlezbmzeduk\nrpepcfpelvhzzxzj\nzxbovsmixfvmamnj\ncpkabmaahbnlrhiz\njvomcbqeoqrmynjj\niqdeisnegnkrkdws\nilhemlrtxdsdnirr\nfjimtscrwbfuwmpo\nlmfiylebtzwtztmx\nddouhysvomrkcpgu\nxtjwvzdhgnwwauwi\ncntzuwcumbsebwyy\nhieqvdlvnxkygeda\nhushfszxskjdrjxi\nxvdfzqblccfoxvyq\nnldnrtieteunyxnb\nvszpidfocenlhzqb\nofcuvtwhortxesoq\nbwniqemqwxlejcfq\nwkqiwdjnytjnomps\nrbadoommlmrictte\nnsmxhpothlulxivt\nbvzbfcvenskqxejr\nsdqeczmzpqqtqabq\nbjveyzniaaliatkw\nzxsqlntyjajjxytk\njkoxlerbtidsuepg\newtlibdkeqwgxnqt\nlmrshemwxrdwzrgc\nnekcdyxmftlymfir\nedaqvmulzkskzsfy\nznmvqaupykjmyebx\nximtebuxwhqpzubd\nrrlstppkknqyxlho\nuyibwcitxixjfwcr\nchrvoierkimesqmm\ndltxmwhheldvxwqe\nxfuthxjuuizanfjy\nvtiwavmxwonpkpug\nphchnujfnxewglht\nowvmetdjcynohxtw\ncbtujdrumixxatry\niirzildsfxipfipe\nsqxcscqyofohotcy\nsbubnekndkvovuqg\njzhsqqxqdrtibtcd\nmscwasyvxkhlvwbn\nbpafxtagbuxivbwz\nuhvueesygaxrqffw\ntrrxlibhtmzuwkkl\nyktkmkokmfslgkml\ngfzzzdptaktytnqg\npgqmaiwzhplnbyhg\nqjiptlkwfshunsfb\nlewvlpescsyunxck\ntywsfatykshogjas\nqtrnwjjgxdektjgi\narypcritpwijczkn\njwxvngigbhfpiubf\nupsjdctitlbqlnhf\nlvpjlrpnmdjiscrq\njvzchdrsnkgpgsti\nwuoesbwunpseyqzu\nxuqspvoshgxmrnrb\nicdawnmfnpnmyzof\nhwcwtibgpvctznuo\nbzdjrniddyamfloq\nhffkxtzuazageruv\ndeixfxjvzbitalnc\nzihsohukiqrgsnvw\nnwoondfnlgowavkg\nqnuulsywgnoillgn\nkoozejhfjyzuhviy\noetcoipohymhpump\ncizwpfczfoodwuly\njghlinczhtaxifau\nsvjejifbidnvvdvy\nrxmbsnaqhzcnbfcl\nvveubmiecvdtrket\nsbihpvrcnzjtgfep\niqbuljuxkwrlebvw\nptrhvxrpezqvmmvv\nduwzugnhktpiybjw\nlijafjnujfeflkva\ncoylvegferuuyfop\nfowsjrgammrqkkof\npgmcruaioccmbrbz\nosejwflxagwqtjoi\notqflckqgxzvtper\nslwyntdcrncktoka\nhzcdzsppcfkrblqg\njksdmmvtzkqaompg\ngalwwwgugetdohkg\nzbghtjvuikmfjuef\ndmqwcamjtlcofqib\nzbczldlfdzemxeys\nmdlqoklybhppdkwe\ntuyajhkexrrrvnlb\nylfolaubymxmkowo\nnnsyrfnoyrxswzxn\nzkhunhhhigbsslfk\nspbokzdfkbmflanz\nzmzxvrwdhiegfely\nimywhfczvmgahxwl\nfnvabvxeiqvsarqq\nyschramprctnputs\nubyjrgdzsvxzvouj\nqnvdhpptympctfer\nsmipxcntyhjpowug\nouhjibgcmotegljy\nzpflubaijjqqsptz\nfgysnxrnfnxprdmf\npbpznrexzxomzfvj\nthhzjresjpmnwtdv\nsbmokolkhvbfqmua\nsxxpdohxlezmqhhx\npevvsyqgoirixtqh\nwdxrornmhqsbfznb\nzjqziqbctxkshqcn\nnbqcwpzfwfaahylk\nbxbvkonpcxprxqjf\nxplbpqcnwzwqxheb\nprsakggmnjibrpoy\nxoguxbpnrvyqarjl\nilrgryrmgwjvpzjy\nefwrmokaoigjtrij\nyhcncebopycjzuli\ngwcmzbzaissohjgn\nlggmemwbbjuijtcf\nfkqedbfrluvkrwwl\njcbppekecevkwpuk\nonvolrckkxeyzfjt\nzzousprgrmllxboy\ncajthmamvxuesujl\nrmiozfsikufkntpg\nlvekypkwjbpddkcv\ndwaqzfnzcnabersa\npcdsskjopcqwhyis\nuabepbrrnxfbpyvx\nyxlgdomczciiunrk\nccerskfzctqxvrkz\nedvmkntljlncwhax\nxtcbwecdwygrvowo\naxqgqjqkqwrgcqot\ntyjrynolpzqwnjgj\nthrtmlegdjsuofga\nmpgoeqkzzqqugait\nemuslxgoefdjyivl\nklehpcehdznpssfb\nxfgvugyrdxolixkc\nacenyrbdwxywmwst\nyqgperajsfsamgan\ndbjxlnumrmhipquw\nhsnhirmswcenewxm\nqehqkbhmgucjjpwo\ngprjdglsbtsfzqcw\nwvqkyrkoratfmvfi\nmyhzlerupqbduqsl\ncouyazesiuhwwhht\nscxzehubxhkfejrr\ngqlitwfriqkmzqdd\npxtbmqelssoagxko\ndzhklewjqzmrfzsw\nyxgeypduywntnbji\nkwzbgzhkzbgedlfh\nvukmuyfstgmscuab\nvcmaybfvdgwnasgt\nqmybkqqdhjigzmum\ncbnuicuncvczyalu\nqdgpsdpdlgjasjqr\nkdzxqqheurupejjo\nmcatrxfchbqnxelm\nbadunwkeggdkcgco\nntaeanvcylpoqmxi\nghnyfytpzgvuokjn\nozepydixmjijdmts\nqefcfwzdhwmcyfvp\nycyktmpaqgaxqsxt\nedpizkxnsxeeebfl\nuwciveajsxxwoqyr\nrbvjkljpxtglqjsh\nnbplrskduutrptfk\nvewrbadvkseuloec\nupaotnjxquomoflx\nqfwxkinrousqywdd\nmqzxvvskslbxvyjt\noxicszyiqifoyugx\nbkitxwzjpabvhraj\nydrbyjecggynjpir\nhezyteaublxxpamq\nhxkuektnoovsehnd\ncwtbbavnhlpiknza\nqrwvkhbyasgfxwol\nqryjbohkprfazczc\nwjksnogpxracrbud\nznmsxbhliqxhvesr\ngkippedrjzmnnwkp\npklylwsnsyyxwcwg\nosdpwbxoegwaiemr\nkpslrrrljgtjiqka\nvuqkloqucpyzfxgk\nbvtdsisgvkuzghyl\nqlcayluuyvlhdfyy\nkbimqwnzanlygaya\nnvoeanlcfhczijed\nkqvcijcuobtdwvou\npmhdpcmxnprixitl\nyueilssewzabzmij\nzqxhafrvjyeyznyg\nmhdounmxkvnnsekx\nhnacyglnzicxjakg\niaxfdqibnrcjdlyl\niypoelspioegrwix\nuiqouxzmlnjxnbqt\nkslgjfmofraorvjo\nbgvotsdqcdlpkynk\nhuwcgxhvrrbvmmth\nvpqyfnkqqjacpffw\nhpjgdfovgmrzvrcl\nvbntbhbvdeszihzj\nnrbyyuviwyildzuw\nwckeoadqzsdnsbox\nxgsobwuseofxsxox\nanvhsxdshndembsd\niygmhbegrwqbqerg\nylrsnwtmdsrgsvlh\nzvvejnrarsavahvc\nyncxhmmdtxxeafby\nkekgiglblctktnes\nuoqgymsrlrwdruzc\nsaaoymtmnykusicw\nbqvcworpqimwglcp\nzbpgtheydoyzipjv\npkykzslwsjbhcvcj\njhwxxneyuuidrzvl\npafeyajcrlehmant\nklszcvtmcdeyfsmj\nledsltggvrbvlefn\nhubpbvxknepammep\ngthxhaapfpgtilal\njtfhbozlometwztj\njrhshycyenurbpwb\nfyaxbawrsievljqv\nlgfcgbenlqxqcxsd\ndhedabbwbdbpfmxp\nmxzgwhaqobyvckcm\nqboxojoykxvwexav\njcpzfjnmvguwjnum\nohpsxnspfwxkkuqe\nnyekrqjlizztwjqp\nthuynotacpxjzroj\nwymbolrlwosnbxqx\niyaqihnqvewxdtjm\nhdvdbtvfpdrejenu\ngtjscincktlwwkkf\nwtebigbaythklkbd";

            string[] inputStrings = input.Split('\n');

            int nice1Count = inputStrings.Count(i => IsNice1(i));
            Console.WriteLine("Answer 1: {0}", nice1Count);

            int nice2Count = inputStrings.Count(i => IsNice2(i));
            Console.WriteLine("Answer 2: {0}", nice2Count);

            Console.ReadKey();
        }
    }
}
