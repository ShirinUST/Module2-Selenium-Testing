using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFashion_Selenium_Project.Utilities
{
    internal class ExcelUtils
    {
        public static List<ExcelProductSearch> ReadExcelData(string excelFilePath, string sheetName)
        {
            List<ExcelProductSearch> excelDataList = new List<ExcelProductSearch>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ExcelProductSearch excelData = new ExcelProductSearch
                            {
                                SearchText = GetValueOrDefault(row, "searchtext"),
                                Category = GetValueOrDefault(row, "category"),
                                ProductId = GetValueOrDefault(row, "productid"),
                                MinCost = GetValueOrDefault(row, "mincost"),
                                MaxCost = GetValueOrDefault(row, "maxcost"),
                                
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }
        //For Review
        public static List<ExcelReviewProduct> ReadExcelDataReview(string excelFilePath, string sheetName)
        {
            List<ExcelReviewProduct> excelDataList = new List<ExcelReviewProduct>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ExcelReviewProduct excelData = new ExcelReviewProduct
                            {
                                SearchText = GetValueOrDefault(row, "searchitem"),
                                Rating = GetValueOrDefault(row, "rating"),
                                Description = GetValueOrDefault(row, "description"),
                                Title = GetValueOrDefault(row, "title"),
                                ProductId= GetValueOrDefault(row,"id"),
                                
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }
        //For Gift Card
        public static List<ExcelGiftCard> ReadExcelDataGiftCard(string excelFilePath, string sheetName)
        {
            List<ExcelGiftCard> excelDataList = new List<ExcelGiftCard>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ExcelGiftCard excelData = new ExcelGiftCard
                            {
                                Money = GetValueOrDefault(row, "money"),
                                Quantity = GetValueOrDefault(row, "quantity"),
                                Delivery = GetValueOrDefault(row, "delivery"),
                                Mode = GetValueOrDefault(row, "mode"),
                                FirstName = GetValueOrDefault(row, "fname"),
                                LastName = GetValueOrDefault(row, "lname"),
                                Email = GetValueOrDefault(row, "email"),
                                Mobile = GetValueOrDefault(row, "mobile"),
                                Message = GetValueOrDefault(row, "message"),
                            };

                            excelDataList.Add(excelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelDataList;
        }

        static string GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }

    }
}
