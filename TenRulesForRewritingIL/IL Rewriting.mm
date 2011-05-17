<map version="0.9.0">
<!-- To view this file, download free mind mapping software FreeMind from http://freemind.sourceforge.net -->
<node CREATED="1305593891816" ID="ID_1383483370" MODIFIED="1305617628026" TEXT="IL Rewriting">
<node CREATED="1305617846223" ID="ID_1841506975" MODIFIED="1305618399278" POSITION="right" TEXT="Intro: Why learn IL Rewriting?">
<node CREATED="1305618323724" ID="ID_687347288" MODIFIED="1305618330514" TEXT="Architectural changes">
<node CREATED="1305618344003" ID="ID_1557286054" MODIFIED="1305618349040" TEXT="Mocking for testability"/>
<node CREATED="1305618362362" ID="ID_937812564" MODIFIED="1305618373504" TEXT="Crosscutting concerns (AOP)"/>
</node>
<node CREATED="1305618508060" ID="ID_317928057" MODIFIED="1305618519361" TEXT="Gain a better understanding of the CLR"/>
<node CREATED="1305618644951" ID="ID_1649155461" MODIFIED="1305618655308" TEXT="Learn how to create your own programming language"/>
</node>
<node CREATED="1305606534135" ID="ID_898399689" MODIFIED="1305619437256" POSITION="right" TEXT="Intro: Data on the CLR">
<node CREATED="1305606557998" ID="ID_1648657633" MODIFIED="1305606560996" TEXT="Fields"/>
<node CREATED="1305606563966" ID="ID_1457358935" MODIFIED="1305606568747" TEXT="Local variables"/>
<node CREATED="1305606571558" ID="ID_599412752" MODIFIED="1305606583994" TEXT="the CLR execution stack"/>
</node>
<node CREATED="1305605794295" ID="ID_381840299" MODIFIED="1305619436504" POSITION="right" TEXT="Rule 0: Tools of the trade">
<node CREATED="1305605809797" ID="ID_1112546341" MODIFIED="1305605817947" TEXT="PEVerify.exe"/>
<node CREATED="1305605822197" ID="ID_746935704" MODIFIED="1305605829226" TEXT="Mono.Cecil"/>
<node CREATED="1305605830996" ID="ID_153791483" MODIFIED="1305605851713" TEXT="A .NET Disassembler">
<node CREATED="1305605853324" ID="ID_1017167721" MODIFIED="1305605858240" TEXT="ILdasm"/>
<node CREATED="1305605859059" ID="ID_1396877936" MODIFIED="1305605866736" TEXT="ILSpy"/>
</node>
</node>
<node CREATED="1305606170590" ID="ID_898988180" MODIFIED="1305619430369" POSITION="left" TEXT="Rule 1: Learn IL from the compiler">
<node CREATED="1305606317624" ID="ID_1760727609" MODIFIED="1305606322294" TEXT="Write it in C#"/>
<node CREATED="1305606323727" ID="ID_142137275" MODIFIED="1305606328790" TEXT="Disassemble it"/>
<node CREATED="1305606329552" ID="ID_798778444" MODIFIED="1305606334197" TEXT="Mimic the code in Cecil"/>
</node>
<node CREATED="1305606412580" ID="ID_1228896192" MODIFIED="1305619429609" POSITION="left" TEXT="Rule 2: Always balance the stack; One push = one pop">
<node CREATED="1305606477833" ID="ID_1470241680" MODIFIED="1305616926335" TEXT="Common stack errors">
<node CREATED="1305606491729" ID="ID_76033506" MODIFIED="1305606495438" TEXT="Stack overflow"/>
<node CREATED="1305606498992" ID="ID_331821936" MODIFIED="1305606501406" TEXT="Stack underflow"/>
<node CREATED="1305606504465" ID="ID_609730841" MODIFIED="1305606507126" TEXT="Wrong type on stack"/>
</node>
<node CREATED="1305607217579" ID="ID_1614233494" MODIFIED="1305616925176" TEXT="You can insert any instruction as long as it keeps the stack balanced">
<node CREATED="1305607366909" ID="ID_171760935" MODIFIED="1305607373385" TEXT="Example: OpCodes.Nop"/>
<node CREATED="1305607495759" ID="ID_1517276677" MODIFIED="1305619628832" TEXT="Common CIL Instructions and how they affect the stack">
<node CREATED="1305607691832" ID="ID_1942136181" MODIFIED="1305607694493" TEXT="Method calls">
<node CREATED="1305607697351" ID="ID_279529743" MODIFIED="1305607702972" TEXT="One push per method parameter"/>
<node CREATED="1305607708911" ID="ID_568844314" MODIFIED="1305607717868" TEXT="One extra stack push for instance methods"/>
<node CREATED="1305607723670" ID="ID_383581230" MODIFIED="1305607749299" TEXT="Methods with return values push the return value onto the stack"/>
</node>
<node CREATED="1305607765669" FOLDED="true" ID="ID_1750051070" MODIFIED="1305608344830" TEXT="Branching instructions">
<node CREATED="1305607854537" ID="ID_1937666713" MODIFIED="1305607871223" TEXT="Brance if false/true">
<node CREATED="1305607873400" ID="ID_1631290196" MODIFIED="1305607882607" TEXT="Pops a boolean value off the stack"/>
</node>
</node>
<node CREATED="1305608074944" FOLDED="true" ID="ID_1660507071" MODIFIED="1305608366605" TEXT="Storing/Loading arguments, variables, and fields">
<node CREATED="1305608122374" ID="ID_1913262780" MODIFIED="1305608144748" TEXT="Storing = popping info off the stack"/>
<node CREATED="1305608148205" ID="ID_1859988710" MODIFIED="1305608155995" TEXT="Loading = pushing info onto the stack"/>
<node CREATED="1305608285152" ID="ID_17146938" MODIFIED="1305608304052" TEXT="Instance fields need a target instance pushed onto the stack"/>
</node>
</node>
<node CREATED="1305607914422" ID="ID_150518242" MODIFIED="1305607932812" TEXT="Stack balancing means the number of stack pushes = stack pops"/>
</node>
</node>
<node CREATED="1305608453360" ID="ID_368379006" MODIFIED="1305619429049" POSITION="left" TEXT="Rule 3: Don&apos;t forget to explicitly box and unbox your types">
<node CREATED="1305608472111" ID="ID_280397863" MODIFIED="1305608485043" TEXT="Boxing = converting a value type to object"/>
<node CREATED="1305608487095" ID="ID_1144925388" MODIFIED="1305608513916" TEXT="Unboxing = casting an object back to a value type">
<node CREATED="1305608601771" ID="ID_718363491" MODIFIED="1305608626983" TEXT="OpCodes.UnboxAny is the easy way to unbox types"/>
</node>
</node>
<node CREATED="1305608706510" ID="ID_1634812469" MODIFIED="1305619428337" POSITION="left" TEXT="Rule 4: PEVerify can be your best friend and worst enemy at the same time">
<node CREATED="1305608729901" ID="ID_435293254" MODIFIED="1305608792601" TEXT="Most PEVerify errors will be due to imbalanced stacks"/>
<node CREATED="1305608825586" ID="ID_222058648" MODIFIED="1305608860365" TEXT="PEVerify is the only tool available for verifying assemblies"/>
<node CREATED="1305608972355" ID="ID_1727748777" MODIFIED="1305608982153" TEXT="PEVerify errors are often cryptic"/>
</node>
<node CREATED="1305610595769" ID="ID_879288298" MODIFIED="1305619426673" POSITION="left" TEXT="Rule 5: Use Console.WriteLine() to spot errors in your IL at runtime">
<node CREATED="1305610637191" ID="ID_1746952266" MODIFIED="1305610668828" TEXT="Good for pinpointing logical errors in dynamic IL"/>
<node CREATED="1305610700645" ID="ID_735661911" MODIFIED="1305611326914" TEXT="Use Beginning, Middle, and End markers in Console.WriteLine">
<node CREATED="1305611339306" ID="ID_1207234725" MODIFIED="1305611367655" TEXT="A binary search pinpoints the target"/>
</node>
</node>
<node CREATED="1305611408247" ID="ID_1829410661" MODIFIED="1305619435761" POSITION="right" TEXT="Rule 6: GOTO statements are a good thing(tm)">
<node CREATED="1305611828414" ID="ID_1093131212" MODIFIED="1305611861307" TEXT="Each branch statement points to the next instruction">
<node CREATED="1305611878500" ID="ID_502358774" MODIFIED="1305611895697" TEXT="OpCodes.Nop is the best branch target"/>
</node>
<node CREATED="1305611914963" ID="ID_1341657705" MODIFIED="1305611925945" TEXT="Inserting additional branch statements">
<node CREATED="1305611958410" ID="ID_1765774107" MODIFIED="1305611966606" TEXT="Jumping around existing code"/>
<node CREATED="1305611985600" ID="ID_146032012" MODIFIED="1305611992558" TEXT="Skipping code under certain conditions"/>
<node CREATED="1305612055621" ID="ID_1303619509" MODIFIED="1305612070722" TEXT="Common cases for adding branch instructions">
<node CREATED="1305612022254" ID="ID_1765263869" MODIFIED="1305612104745" TEXT="Skipping code as a result of a boolean value from a method"/>
</node>
</node>
</node>
<node CREATED="1305612783008" ID="ID_1682447676" MODIFIED="1305619435112" POSITION="right" TEXT="Rule 7: Emit as few IL instructions as possible">
<node CREATED="1305612809262" ID="ID_997392275" MODIFIED="1305612818028" TEXT="Not everything has to be in IL"/>
<node CREATED="1305615312864" ID="ID_183787743" MODIFIED="1305615336629" TEXT="Emit only branches and simple method calls"/>
<node CREATED="1305615351614" ID="ID_1456349225" MODIFIED="1305615374843" TEXT="Fewer IL instructions = easier debugging"/>
<node CREATED="1305616628850" ID="ID_621771067" MODIFIED="1305616651222" TEXT="If you don&apos;t know how to emit the IL, then delegate it to a static method call"/>
</node>
<node CREATED="1305615440195" ID="ID_750928137" MODIFIED="1305619433584" POSITION="right" TEXT="Rule 8: Almost anything can be modified, including sealed classes and final methods">
<node CREATED="1305615968597" ID="ID_774946434" MODIFIED="1305616001394" TEXT="Many restrictions are enforced by the C# compiler, not the CLR">
<node CREATED="1305616613474" ID="ID_979179715" MODIFIED="1305616623800" TEXT="Cecil allows you to change everything about a method"/>
<node CREATED="1305616878199" ID="ID_965981420" MODIFIED="1305616912348" TEXT="Even the &apos;sealed&apos; property of a method is something that you can change"/>
</node>
<node CREATED="1305616081433" ID="ID_1096505921" MODIFIED="1305616125957" TEXT="*You* are the compiler"/>
</node>
<node CREATED="1305616950620" ID="ID_1530272459" MODIFIED="1305619433032" POSITION="right" TEXT="Rule 9: You can replace any method as long as you keep the stack balanced">
<node CREATED="1305617256424" ID="ID_884657068" MODIFIED="1305617299213" TEXT="You must have the correct number of stack pop operations where the method call is made"/>
<node CREATED="1305617303125" ID="ID_1737639670" MODIFIED="1305617336338" TEXT="You must have the correct number (and type) of stack push operations if the method has a return value"/>
<node CREATED="1305617397650" ID="ID_79472387" MODIFIED="1305617405216" TEXT="Example: Swapping one static method call for another"/>
</node>
<node CREATED="1305617663863" ID="ID_1806198452" MODIFIED="1305617679324" POSITION="right" TEXT="Rule 10: IL can be learned like any other language; practice makes perfect"/>
<node CREATED="1305619100284" ID="ID_657346601" MODIFIED="1305619616593" POSITION="left" TEXT="Where to go from here">
<node CREATED="1305619112460" ID="ID_1074495767" MODIFIED="1305619623394" TEXT="References">
<node CREATED="1305619126883" ID="ID_1700399656" MODIFIED="1305619138145" TEXT="ECMA 335 Specification"/>
<node CREATED="1305619140730" ID="ID_1569057889" MODIFIED="1305619157888" TEXT="Serge Lidin - Expert .NET 2.0 IL Assembler"/>
<node CREATED="1305619205512" ID="ID_89249157" MODIFIED="1305619219894" TEXT="My blog: http://plaureano.blogspot.com"/>
<node CREATED="1305619397728" ID="ID_798207057" MODIFIED="1305619411070" TEXT="Really easy logging using IL Rewriting and the .NET Profiling API: http://www.codeproject.com/KB/cs/IL_Rewriting.aspx"/>
</node>
<node CREATED="1305619120571" ID="ID_796173634" MODIFIED="1305619622832" TEXT="Contact me">
<node CREATED="1305619222847" ID="ID_1217663952" MODIFIED="1305619238237" TEXT="Twitter: http://twitter.com/philiplaureano"/>
</node>
</node>
</node>
</map>
