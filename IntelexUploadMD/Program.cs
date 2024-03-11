using IntelexUploadMD.ConsoleApp;
using IntelexUploadMD.Services.ExtractHelper.DataExtractHelper;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main()
    {
        //try
        //{
        //    // Create an instance of SubLocation1ToIntelex
        //    var subLocation1ToIntelex = new SubLocation1ToIntelex(new AssetExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await subLocation1ToIntelex.SubLocation1ToIntelexFunc();

        //    Console.WriteLine("Data synchronization for SubLocation1 completed successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}

        //try
        //{
        //    // Create an instance of SubLocation2ToIntelex
        //    var subLocation2ToIntelex = new SubLocation2ToIntelex(new AssetExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await subLocation2ToIntelex.SubLocation2ToIntelexFunc();

        //    Console.WriteLine("Data synchronization completed for SubLocation2 successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}


        //try
        //{
        //    // Create an instance of SubLocation3ToIntelex
        //    var subLocation3ToIntelex = new SubLocation3ToIntelex(new AssetExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await subLocation3ToIntelex.SubLocation3ToIntelexFunc();

        //    Console.WriteLine("Data synchronization completed for SubLocation3 successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}

        //try
        //{
        //    // Create an instance of SubLocation2ToIntelex
        //    var subLocation2ToIntelexDelete = new SubLocation2ToIntelexDelete(new AssetExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await subLocation2ToIntelexDelete.SubLocation2ToIntelexDeleteFunc();

        //    Console.WriteLine("Data deletion completed for SubLocation2 successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}

        //try
        //{
        //    // Create an instance of SubLocation2ToIntelex
        //    var subLocation3ToIntelexDelete = new SubLocation3ToIntelexDelete(new AssetExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await subLocation3ToIntelexDelete.SubLocation3ToIntelexDeleteFunc();

        //    Console.WriteLine("Data deletion completed for SubLocation3 successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}

        //try
        //{
        //    // Create an instance of SubLocation3ToIntelex
        //    var SupplierToIntelex = new SupplierToIntelex(new SupplierExtractHelper()); //SubLocation1ExtractHelper

        //    // Call the asynchronous method on the instance
        //    await SupplierToIntelex.SupplierToIntelexFunc();

        //    Console.WriteLine("Data synchronization completed for Supplier successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}

        //try
        //{
        //    IConfiguration configuration = new ConfigurationBuilder()
        //        .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
        //        .Build();

        //    var supplierExtractHelper = new SupplierExtractHelper(); // Or however you initialize this
        //    var supplierToIntelex = new SupplierToIntelex(supplierExtractHelper, configuration);

        //    await supplierToIntelex.SupplierToIntelexFunc();

        //    Console.WriteLine("Data synchronization for Supplier completed successfully.");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}


        try
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                .Build();

            var supplierExtractHelper = new SupplierExtractHelper(); // Or however you initialize this
            var supplierToIntelex = new SupplierToIntelexDelete(supplierExtractHelper, configuration);

            await supplierToIntelex.SupplierToIntelexFunc();

            Console.WriteLine("Data Deletion for Supplier completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }



    }
}
