using System;
using System.Windows.Controls;
namespace Multicriteria_model
{
    /// <summary>
    /// Страница с критериями
    /// </summary>
    public partial class Criteria : Page
    {
        private readonly Task _parentPage;
        private readonly string _productType;
        /// <summary>
        /// Страница с критериями
        /// </summary>
        /// <param name="parentPage">Родительская страница</param>
        /// <param name="productType">Тип товара</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Criteria(Task parentPage, string productType)
        {
            InitializeComponent();
            _parentPage = parentPage ?? throw new ArgumentNullException(nameof(_parentPage),
                "Ошибка при создании страницы с критериями:\nОтсутствует ссылка на родительскую страницу!");
            _productType = productType ?? throw new ArgumentNullException(nameof(_productType),
                "Ошибка при создании страницы с критериями:\nОтсутствует тип товара!");
            CreateCriteriaList();
        }
        private void CreateCriteriaList()
        {
            Product[] productList = Server.GetProducts(_productType).ToArray();
            int index = 0;
            foreach (var criteria in productList[0].Characteristics)
            {
                criteriaListBox.Items.Add(new CriteriaElement(criteria.Name, criteria.Value));
                index++;
            }
        }
    }
}