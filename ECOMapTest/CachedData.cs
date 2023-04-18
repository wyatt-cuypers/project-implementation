using ECOMap;
namespace ECOMapTest
{
	internal static class CachedData
	{
		private static ECODataService _service = null;
		private static LoadingProcessor _processor = new LoadingProcessor(EcoGeneralService.InitialPathLocation);

		internal static async Task<ECODataService> GetECODataService()
		{
			if (_service == null)
			{
				// DESIGN NOTE: This directly reads from our overall large data source.  One could easily copy a few lines from each file as it pertains to
				// tests for mocking purposes and test specific scenarios.
				_service = await _processor.LoadAll();
			}

			return _service;
		}
	}
}
