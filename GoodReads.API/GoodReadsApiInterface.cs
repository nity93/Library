using GoodReads.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodReads.API
{
    public static class GoodReadsApiInterface
    {
        public static async Task<List<Work>> GetBookAsync(string bookTitle, AuthenticationToken authToken)
        {
            using (var client = new GoodReadsApiClient(authToken))
            {
               return await client.GetBookAsync(bookTitle);
            }
        }
    }
}
