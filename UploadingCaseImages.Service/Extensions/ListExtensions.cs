namespace UploadingCaseImages.Service.Extensions;

public static class ListExtensions
{
	public static string SeparateAsString<T>(this List<T> source)
	{
		if (source != null && source.Count > 0)
		{
			return string.Join(',', source);
		}

		return string.Empty;
	}

	public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
	{
		if (chunkSize <= 0)
			throw new ArgumentException("Chunk size must be greater than 0.", nameof(chunkSize));

		var total = 0;

		var chunkedList = new List<List<T>>();

		while (total < source.Count)
		{
			var chunk = source
				.Skip(total)
				.Take(chunkSize);

			chunkedList.Add(chunk.ToList());

			total += chunkSize;
		}

		return chunkedList;
	}
	public static List<T> FilterBy<T>(this List<T> list, System.Func<T, bool> predicate)
	{
		return list.Where(predicate).ToList();
	}
}
