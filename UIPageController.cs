using System.Collections.Generic;
using UnityEngine;

namespace BluePixel.UISystem
{
    public class UIPageController : MonoBehaviour
    {
        [SerializeField] private UIPage[] _pages;
        [SerializeField] private UIPage _initialPage; // Optional

        private Stack<UIPage> _pageHistory = new();

        private void Awake()
        {
            foreach (var page in _pages)
            {
                page.Initialize();
                page.Close();
            }

            AddPage(_initialPage);
        }

        public void AddPage(UIPage page)
        {
            if (page == null) return;
            if (_pageHistory.Count > 0 && _pageHistory.Peek() == page) return;

            if (_pageHistory.Count > 0)
            {
                var prevPage = _pageHistory.Peek();
                if (prevPage.DisableWhenNextPageOpens)
                    prevPage.Close();
            }

            _pageHistory.Push(page);
            page.Open(true, true);
        }

        public void RemovePage()
        {
            if (_pageHistory.Count <= 0) return;

            var currentPage = _pageHistory.Pop();
            currentPage.Close(true, true);

            if (_pageHistory.Count > 0)
            {
                var prevPage = _pageHistory.Peek();
                if (prevPage.DisableWhenNextPageOpens)
                    prevPage.Open();
            }
        }

        public void RemoveAllPages()
        {
            while (_pageHistory.Count > 0)
            {
                //var currentPage = _pageHistory.Pop();
                //currentPage.Close();

                _pageHistory.Pop().Close();
            }
        }

        public void RemoveAllAndAddPage(UIPage page)
        {
            RemoveAllPages();
            AddPage(page);
        }
    }
}
