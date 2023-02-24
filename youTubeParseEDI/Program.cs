using System;
using System.Text;
using EdiEngine;
using EdiEngine.Runtime;
using EdiEngine.Standards.X12_004010.Segments;
using Newtonsoft.Json;

namespace youTubeParseEDI;
class Program
{
    static async Task Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");
        Dtm[] dtm = GetDTMdescription("/Users/victorpacheco/Desktop/YouTube/convertjson.json");

        string data = await ReadTextFileAsync("/Users/victorpacheco/Desktop/YouTube/sample.txt");



        //Console.WriteLine(data);
        Parse850(data, dtm);


        //Console.ReadLine();
    }

    static void Parse850(string edi, Dtm[] dtms)
    {

        EdiDataReader r = new EdiDataReader();
        EdiBatch b = r.FromString(edi);

        //Serialize the whole batch to JSON
        JsonDataWriter w1 = new JsonDataWriter();
        string json = w1.WriteToString(b);

        Console.WriteLine(json);

        Console.WriteLine("-----");


        var items = ExportData(b.Interchanges, dtms);



        File.WriteAllText("/Users/victorpacheco/Desktop/YouTube/sample-items-po-" + items[1] + ".csv", items[0]);
    }

    static string[] ExportData(List<EdiInterchange> ediInterchange, Dtm[] dtms)
    {
        var sb = new StringBuilder();
        var sbDates = new StringBuilder();
        string deli = ",";
        string po = string.Empty;

        foreach (var interchange in ediInterchange)
        {
            foreach (var group in interchange.Groups)
            {

                foreach (var transaction in group.Transactions)
                {
                    po = string.Empty;
                    foreach (var content in transaction.Content)
                    {
                        if (content.Name.Equals("BEG"))
                        {

                            var poData = content as EdiSegment;
                            if (poData != null)
                            {
                                var poSegment = poData.Content as List<DataElementBase>;

                                po = poSegment[2].Val;
                            }



                        }

                        if (content.Name.Equals("DTM"))
                        {

                            var dtmData = content as EdiSegment;
                            if (dtmData != null)
                            {
                                var dtmSegment = dtmData.Content as List<DataElementBase>;

                                for (int i = 0; i < dtmSegment.Count - 1; i++)
                                {
                                    var desCode = dtmSegment[0].Val;
                                    var dat = dtmSegment[1].Val;

                                    var desc = dtms.FirstOrDefault(c => c.Code.Equals(desCode));

                                    if (desc != null)
                                    {
                                        sbDates.Append(desCode + deli + dat + deli + desc.Description);
                                    }
                                }
                            }



                        }

                        if (content.Name.Equals("L_PO1"))
                        {

                            var items = content as EdiEngine.Runtime.EdiLoop;

                            if (items != null)
                            {

                                foreach (var itemContent in items.Content)
                                {
                                    var segment = itemContent as EdiSegment;

                                    if (segment != null)
                                    {
                                        if (segment.Name.Equals("PO1"))
                                        {

                                            foreach (var segmentContent in segment.Content)
                                            {
                                                var data = segmentContent as EdiSimpleDataElement;
                                                if (data != null)
                                                {
                                                    var value = data.Val;
                                                    sb.Append(value + deli);
                                                }

                                            }

                                            sb.Append(sbDates.ToString());
                                            sb.Append("\n");
                                        }


                                    }

                                }
                            }


                        }
                    }
                }
            }
        }

        return new string[2] { sb.ToString(), po };
    }

    static async Task<string> ReadTextFileAsync(string filename)
    {

        char[] result;
        StringBuilder builder = new StringBuilder();

        using (StreamReader reader = File.OpenText(filename))
        {
            result = new char[reader.BaseStream.Length];
            await reader.ReadAsync(result, 0, (int)reader.BaseStream.Length);
        }


        foreach (char c in result)
        {

            builder.Append(c.ToString().Replace("\n", "").Replace("\r", "").Replace("\\", ""));
        }
        return builder.ToString();
    }

    static Dtm[] GetDTMdescription(string filePath)
    {


        //string data = await ReadTextFileAsync(filePath);
        var data = File.ReadAllText(filePath);

        var dtms = JsonConvert.DeserializeObject<Dtm[]>(data);

        return dtms;
    }
}

public class Dtm
{
    public string Code;
    public string Description;
}


