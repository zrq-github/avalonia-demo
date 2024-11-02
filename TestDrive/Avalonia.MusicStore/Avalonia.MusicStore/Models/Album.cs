﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using iTunesSearch.Library;

namespace Avalonia.MusicStore.Models;

public class Album
{
    private static readonly iTunesSearchManager s_SearchManager = new();

    private static readonly HttpClient s_httpClient = new();

    public Album(string artist, string title, string coverUrl)
    {
        Artist = artist;
        Title = title;
        CoverUrl = coverUrl;
    }

    private string CachePath => $"./Cache/{Artist} - {Title}";

    public string Artist { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }

    public async Task<Stream> LoadCoverBitmapAsync()
    {
        if (File.Exists(CachePath + ".bmp")) 
            return File.OpenRead(CachePath + ".bmp");

        var data = await s_httpClient.GetByteArrayAsync(CoverUrl);
        return new MemoryStream(data);
    }

    public static async Task<IEnumerable<Album>> SearchAsync(string searchTerm)
    {
        var query = await s_SearchManager.GetAlbumsAsync(searchTerm)
            .ConfigureAwait(false);

        return query.Albums.Select(x =>
            new Album(x.ArtistName, x.CollectionName,
                x.ArtworkUrl100.Replace("100x100bb", "600x600bb")));
    }
}