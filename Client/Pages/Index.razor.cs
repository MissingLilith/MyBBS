﻿using BootstrapBlazor.Components;
using SchoolBBS.Shared;
using System.Net.Http.Json;

namespace SchoolBBS.Client.Pages
{
    public partial class Index
    {
        /// <summary>
        /// Images
        /// </summary>
        private static List<string> Images => new()
    {
        "/images/banner1.jpg",
        "/images/banner2.png",
        "/images/banner3.jpg"
    };
        /// <summary>
        /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
        /// Foo class is used for Demo test, please download the source code if necessary
        /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
        /// </summary>
        private List<PostListModel> Item = new();
        private static IEnumerable<int> PageItemsSource => new int[] { 7 };
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Item = await httpClient.GetFromJsonAsync<List<PostListModel>>("api/Post/GetAllPosts");
        }
        private Task<QueryData<PostListModel>> OnQueryAsync(QueryPageOptions options)
        {
            IEnumerable<PostListModel> items = Item;
            var total = items.Count();
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
            return Task.FromResult(new QueryData<PostListModel>()
            {
                Items = items,
                TotalCount = total,
            });
        }
        private Task ClickRow(PostListModel item)
        {
            Navigation.NavigateTo($"/postdetail?postId={item.Id}");
            return Task.CompletedTask;
        }
    }
}