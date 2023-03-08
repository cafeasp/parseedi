using System;
using EdiEngine.Standards.X12_004010.Maps;
using System.Linq;
using EdiEngine;
using EdiEngine.Common.Definitions;
using EdiEngine.Runtime;
using SegmentDefinitions = EdiEngine.Standards.X12_004010.Segments;
using EdiEngine.Standards.X12_004010.Segments;

namespace youTubeParseEDI
{
	public class AdvanceShipNotice
	{
		
		public string Generate() {

            M_856 map = new M_856();
            EdiTrans t = new EdiTrans(map);

            #region BSN
            // BSN
            var bSN = (MapSegment)map.Content.First(s => s.Name == "BSN");

            var seg = new EdiSegment(bSN);

            string date = DateTime.Now.ToString("yyyyMMdd");

            seg.Content.AddRange(new[] {
                new EdiSimpleDataElement((MapSimpleDataElement)bSN.Content[0], "00"),
                new EdiSimpleDataElement((MapSimpleDataElement)bSN.Content[1], "06"),
                new EdiSimpleDataElement((MapSimpleDataElement)bSN.Content[2], date),
                new EdiSimpleDataElement((MapSimpleDataElement)bSN.Content[3], "1621"),
                new EdiSimpleDataElement((MapSimpleDataElement)bSN.Content[3], "0001")
            });

            t.Content.Add(seg);
            #endregion
            #region DTM_1
            // DTM 0
            var dTM = (MapSegment)map.Content.First(s => s.Name == "DTM");

            seg = new EdiSegment(dTM);

            //shipped
            seg.Content.AddRange(new[] {
                new EdiSimpleDataElement((MapSimpleDataElement)dTM.Content[0], "011"),
                new EdiSimpleDataElement((MapSimpleDataElement)dTM.Content[1], date)
            });

            t.Content.Add(seg);
            #endregion
            #region DTM_2
            //Estimate Delivery
            seg = new EdiSegment(dTM);
            
            var edCode = new EdiSimpleDataElement((MapSimpleDataElement)dTM.Content[0], "017");
            var edDate = new EdiSimpleDataElement((MapSimpleDataElement)dTM.Content[0], date);

            seg.Content.Add(edCode);
            seg.Content.Add(edDate);

            t.Content.Add(seg);
            #endregion
            #region HLLevelLoop
            //HL Hierarchical Level Loop

            var hLloop = (MapLoop)map.Content.First(s => s.Name == "L_HL");

            //HL
            var hl = (MapSegment)hLloop.Content.First(s => s.Name == "HL");
            
            var hlLevel = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[0], "1");
            var hlOptional = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[1], "");
            var hlLevelCode = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[2], "S");

            seg = new EdiSegment(hl);

            seg.Content.Add(hlLevel);
            seg.Content.Add(hlOptional);
            seg.Content.Add(hlLevelCode);

            t.Content.Add(seg);
            #endregion
            #region TD1
            //TD1
            var td1 = (MapSegment)hLloop.Content.First(s => s.Name == "TD1");

            var td_101 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[0], "PLT");
            var td_102 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[1], "1");

            var td_103 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[2], "");
            var td_104 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[3], "");
            var td_105 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[4], "");
            var td_106 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[5], "G");
            var td_107 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[6], "100.0");
            var td_108 = new EdiSimpleDataElement((MapSimpleDataElement)td1.Content[7], "LB");

            seg = new EdiSegment(td1);

            seg.Content.Add(td_101);
            seg.Content.Add(td_102);

            seg.Content.Add(td_103);
            seg.Content.Add(td_104);
            seg.Content.Add(td_105);
            seg.Content.Add(td_106);
            seg.Content.Add(td_107);
            seg.Content.Add(td_108);

            t.Content.Add(seg);
            #endregion
            #region TD5
            //TD5
            var td5 = (MapSegment)hLloop.Content.First(s => s.Name == "TD5");
            var td501 = new EdiSimpleDataElement((MapSimpleDataElement)td5.Content[0], "B");
            var td502 = new EdiSimpleDataElement((MapSimpleDataElement)td5.Content[1], "2");
            var td503 = new EdiSimpleDataElement((MapSimpleDataElement)td5.Content[2], "UPSA");
            var td504 = new EdiSimpleDataElement((MapSimpleDataElement)td5.Content[3], "A");
            var td505 = new EdiSimpleDataElement((MapSimpleDataElement)td5.Content[4], "UPS");

            seg = new EdiSegment(td5);

            seg.Content.Add(td501);
            seg.Content.Add(td502);
            seg.Content.Add(td503);
            seg.Content.Add(td504);
            seg.Content.Add(td505);

            t.Content.Add(seg);
            #endregion
            #region REF_BM
            //REF BM
            var rEF = (MapSegment)hLloop.Content.First(s => s.Name == "REF");
            var rEF01 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[0], "BM");
            var rEF02 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[1], "2023215");

            seg = new EdiSegment(rEF);

            seg.Content.Add(rEF01);
            seg.Content.Add(rEF02);

            t.Content.Add(seg);
            #endregion
            #region REF_YD
            //REF YD
            rEF = (MapSegment)hLloop.Content.First(s => s.Name == "REF");
            rEF01 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[0], "YD");
            rEF02 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[1], "NON-SERIALIZED");

            seg = new EdiSegment(rEF);

            seg.Content.Add(rEF01);
            seg.Content.Add(rEF02);

            t.Content.Add(seg);
            #endregion
            #region REF_ZA
            //REF ZA
            rEF = (MapSegment)hLloop.Content.First(s => s.Name == "REF");
            rEF01 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[0], "ZA");
            rEF02 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[1], "1085");

            seg = new EdiSegment(rEF);

            seg.Content.Add(rEF01);
            seg.Content.Add(rEF02);

            t.Content.Add(seg);
            #endregion


            //N1 Loop
            var N1Loop = (MapLoop)hLloop.Content.First(s => s.Name == "L_N1");

            #region ShipFrom
            //N1
            var N1 = (MapSegment)N1Loop.Content.First(s => s.Name == "N1");
            var N01 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[0], "SF");
            var N02 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[1], "From Company Name");

            var N03 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[2], "92");
            var N04 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[3], "123456");

            seg = new EdiSegment(N1);

            seg.Content.Add(N01);
            seg.Content.Add(N02);
            seg.Content.Add(N03);
            seg.Content.Add(N04);

            t.Content.Add(seg);



            //N3
            var N3 = (MapSegment)N1Loop.Content.First(s => s.Name == "N3");
            var N301 = new EdiSimpleDataElement((MapSimpleDataElement)N3.Content[0], "123 Somewhere");

            seg = new EdiSegment(N3);

            seg.Content.Add(N301);
           

            t.Content.Add(seg);

            //N4
            var N4 = (MapSegment)N1Loop.Content.First(s => s.Name == "N4");
            var N401 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[0], "CityName");
            var N402 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[1], "StateName");

            var N403 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[2], "ZipCode");
            var N404 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[3], "US");

            seg = new EdiSegment(N4);

            seg.Content.Add(N401);
            seg.Content.Add(N402);
            seg.Content.Add(N403);
            seg.Content.Add(N404);

            t.Content.Add(seg);
            #endregion

            #region ShipTo
            //N1
            var NShipTo1 = (MapSegment)N1Loop.Content.First(s => s.Name == "N1");
            var NShipTo01 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[0], "ST");
            var NShipTo02 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[1], "YouTube");

            var NShipTo03 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[2], "92");
            var NShipTo04 = new EdiSimpleDataElement((MapSimpleDataElement)N1.Content[3], "000011111");

            seg = new EdiSegment(NShipTo1);

            seg.Content.Add(NShipTo01);
            seg.Content.Add(NShipTo02);
            seg.Content.Add(NShipTo03);
            seg.Content.Add(NShipTo04);

            t.Content.Add(seg);



            //N3
            var NShipTo3 = (MapSegment)N1Loop.Content.First(s => s.Name == "N3");
            var NShipTo301 = new EdiSimpleDataElement((MapSimpleDataElement)N3.Content[0], "123 Street Suite 19");

            seg = new EdiSegment(NShipTo3);

            seg.Content.Add(NShipTo301);


            t.Content.Add(seg);

            //N4
            var NShipTo4 = (MapSegment)N1Loop.Content.First(s => s.Name == "N4");
            var NShipTo401 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[0], "Indianapolis");
            var NShipTo402 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[1], "IN");

            var NShipTo403 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[2], "111111");
            var NShipTo404 = new EdiSimpleDataElement((MapSimpleDataElement)N4.Content[3], "US");

            seg = new EdiSegment(NShipTo4);

            seg.Content.Add(NShipTo401);
            seg.Content.Add(NShipTo402);
            seg.Content.Add(NShipTo403);
            seg.Content.Add(NShipTo404);

            t.Content.Add(seg);

            #endregion

            #region HL_2nd
            //HL
            hl = (MapSegment)hLloop.Content.First(s => s.Name == "HL");

            hlLevel = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[0], "2");
            hlOptional = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[1], "1");
            hlLevelCode = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[2], "O");

            seg = new EdiSegment(hl);

            seg.Content.Add(hlLevel);
            seg.Content.Add(hlOptional);
            seg.Content.Add(hlLevelCode);

            t.Content.Add(seg);
            #endregion

            #region PRF
            //PRF
            var pRF = (MapSegment)hLloop.Content.First(s => s.Name == "PRF");

            var pRF01 = new EdiSimpleDataElement((MapSimpleDataElement)pRF.Content[0], "00114442");

            seg = new EdiSegment(pRF);

            seg.Content.Add(pRF01);
          

            t.Content.Add(seg);
            #endregion

            #region HL_3nd_T
            //HL
            hl = (MapSegment)hLloop.Content.First(s => s.Name == "HL");

            hlLevel = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[0], "3");
            hlOptional = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[1], "2");
            hlLevelCode = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[2], "T");

            seg = new EdiSegment(hl);

            seg.Content.Add(hlLevel);
            seg.Content.Add(hlOptional);
            seg.Content.Add(hlLevelCode);

            t.Content.Add(seg);
            #endregion

            #region MAN_Pallet
            //MAN
            var man = (MapSegment)hLloop.Content.First(s => s.Name == "MAN");

            var man01 = new EdiSimpleDataElement((MapSimpleDataElement)man.Content[0], "GM");
            var man02 = new EdiSimpleDataElement((MapSimpleDataElement)man.Content[1], "001121212121");

            seg = new EdiSegment(man);

            seg.Content.Add(man01);
            seg.Content.Add(man02);
        

            t.Content.Add(seg);
            #endregion

            #region HL_4nd_T
            //HL
            hl = (MapSegment)hLloop.Content.First(s => s.Name == "HL");

            hlLevel = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[0], "4");
            hlOptional = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[1], "3");
            hlLevelCode = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[2], "P");

            seg = new EdiSegment(hl);

            seg.Content.Add(hlLevel);
            seg.Content.Add(hlOptional);
            seg.Content.Add(hlLevelCode);

            t.Content.Add(seg);
            #endregion


            #region MAN_Case
            //MAN
            man = (MapSegment)hLloop.Content.First(s => s.Name == "MAN");

            man01 = new EdiSimpleDataElement((MapSimpleDataElement)man.Content[0], "GM");
            man02 = new EdiSimpleDataElement((MapSimpleDataElement)man.Content[1], "00000455441111");

            seg = new EdiSegment(man);

            seg.Content.Add(man01);
            seg.Content.Add(man02);


            t.Content.Add(seg);
            #endregion


            #region HL_5nd_Item
            //HL
            hl = (MapSegment)hLloop.Content.First(s => s.Name == "HL");

            hlLevel = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[0], "5");
            hlOptional = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[1], "4");
            hlLevelCode = new EdiSimpleDataElement((MapSimpleDataElement)hl.Content[2], "I");

            seg = new EdiSegment(hl);

            seg.Content.Add(hlLevel);
            seg.Content.Add(hlOptional);
            seg.Content.Add(hlLevelCode);

            t.Content.Add(seg);
            #endregion

            #region LIN
            //LIN
            var lIN = (MapSegment)hLloop.Content.First(s => s.Name == "LIN");

            var lIN01 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[0], "0001");
            var lIN02 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[1], "CB");
            var lIN03 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[2], "ABC123");
            var lIN04 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[3], "MN");
            var lIN05 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[4], "ABC123");
            var lIN06 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[5], "UI");
            var lIN07 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[6], "811111111");
            var lIN08 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[7], "SK");
            var lIN09 = new EdiSimpleDataElement((MapSimpleDataElement)lIN.Content[8], "ABC123");


            seg = new EdiSegment(lIN);

            seg.Content.Add(lIN01);
            seg.Content.Add(lIN02);
            seg.Content.Add(lIN03);
            seg.Content.Add(lIN04);
            seg.Content.Add(lIN05);
            seg.Content.Add(lIN06);
            seg.Content.Add(lIN07);
            seg.Content.Add(lIN08);
            seg.Content.Add(lIN09);

            t.Content.Add(seg);
            #endregion

            #region SN1
            //SN1
            var sN1 = (MapSegment)hLloop.Content.First(s => s.Name == "SN1");

            var sN01 = new EdiSimpleDataElement((MapSimpleDataElement)sN1.Content[0], "0001");
            var sN02 = new EdiSimpleDataElement((MapSimpleDataElement)sN1.Content[1], "228");
            var sN03 = new EdiSimpleDataElement((MapSimpleDataElement)sN1.Content[1], "EA");

            seg = new EdiSegment(sN1);

            seg.Content.Add(sN01);
            seg.Content.Add(sN02);
            seg.Content.Add(sN03);

            t.Content.Add(seg);
            #endregion

            #region PO4
            //PO4
            var pO4 = (MapSegment)hLloop.Content.First(s => s.Name == "PO4");

            var pO01 = new EdiSimpleDataElement((MapSimpleDataElement)pO4.Content[0], "228");

            seg = new EdiSegment(pO4);

            seg.Content.Add(pO01);

            t.Content.Add(seg);
            #endregion

            #region REF_YD
            //REF YD
            rEF = (MapSegment)hLloop.Content.First(s => s.Name == "REF");
            rEF01 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[0], "YD");
            rEF02 = new EdiSimpleDataElement((MapSimpleDataElement)rEF.Content[1], "NON-SERIALIZED");

            seg = new EdiSegment(rEF);

            seg.Content.Add(rEF01);
            seg.Content.Add(rEF02);

            t.Content.Add(seg);
            #endregion

            #region CTT
            //CTT
            var cTT = (MapSegment)map.Content.First(s => s.Name == "CTT");

            var cTT01 = new EdiSimpleDataElement((MapSimpleDataElement)cTT.Content[0], "228");//Quantity
            var cTT02 = new EdiSimpleDataElement((MapSimpleDataElement)cTT.Content[1], "5");//# of HL

            seg = new EdiSegment(cTT);

            seg.Content.Add(cTT01);
            seg.Content.Add(cTT02);
            t.Content.Add(seg);
            #endregion

            var g = new EdiGroup("SH");
            g.Transactions.Add(t);

            var i = new EdiInterchange();
            i.Groups.Add(g);

            EdiBatch b = new EdiBatch();
            b.Interchanges.Add(i);

            //Add all service segments
            //GE*1*200 Assigned number originated and maintained by the sender
            //IEA*1*11111 
            EdiDataWriterSettings settings = new EdiDataWriterSettings(
                new SegmentDefinitions.ISA(), new SegmentDefinitions.IEA(),
                new SegmentDefinitions.GS(), new SegmentDefinitions.GE(),
                new SegmentDefinitions.ST(), new SegmentDefinitions.SE(),
                "ZZ", "SENDER", "ZZ", "RECEIVER", "GSSENDER", "GSRECEIVER",
                "00401", "004010", "T", 100, 200, "~\r\n", "*");

            EdiDataWriter w = new EdiDataWriter(settings);
            //Console.WriteLine(w.WriteToString(b));
            return w.WriteToString(b);

        }
	}
}

