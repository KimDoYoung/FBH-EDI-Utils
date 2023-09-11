using FBH.EDI.Common.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace FBH.EDI.Common
{
    public class EdiUtil
    {
        public static EdiDocument EdiDocumentFromFile(string ediFile)
        {
            if (ediFile.EndsWith("xlsx"))
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(ediFile);
                Excel.Worksheet worksheet = workbook.Worksheets[1];
                try
                {
                    var o = worksheet.GetCell("A1");
                    var a1 = o.ToString().Trim().ToUpper();
                    if (a1.Contains("FREIGHT"))
                    {
                        return Create210(worksheet);
                    }
                    else if (a1.Contains("INQUIRY"))
                    {
                        return Create846(worksheet);
                    }
                    else if (a1.Contains("PURCHASE"))
                    {
                        return Create860(worksheet);
                    }
                    else if (a1.Contains("WAREHOUSE"))
                    {
                        return Create945(worksheet);
                    }
                    else if (a1.Contains("INVOICE"))
                    {
                        return Create810(worksheet);
                    }
                    else
                    {
                        throw new EdiException($"알려지지 않은 EDI 문서 타입입니다.{ediFile}");
                    }
                }
                catch (Exception ex)
                {
                    throw new EdiException(ex.Message);
                }
                finally
                {
                    workbook.Close(false);
                    app.Quit();

                    ReleaseExcelObject(worksheet);
                    ReleaseExcelObject(workbook);
                    ReleaseExcelObject(app);
                }
            }
            else if (ediFile.EndsWith("pdf"))
            {
                return Create210FromPdf(ediFile);
            }
            return null;
        }

        private static EdiDocument Create210FromPdf(string ediFile)
        {
            throw new NotImplementedException();
        }

        private static EdiDocument Create810(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        private static EdiDocument Create945(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        private static EdiDocument Create860(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        private static EdiDocument Create846(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        private static EdiDocument Create210(Worksheet worksheet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 메모리 해제
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
