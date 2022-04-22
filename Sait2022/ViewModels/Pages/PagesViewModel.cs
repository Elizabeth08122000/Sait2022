using System;

namespace Sait2022.ViewModels.Pages
{
    public class PagesViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PagesViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
