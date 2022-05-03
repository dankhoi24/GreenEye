﻿using GreenEye.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEye.ViewModel.Order
{
    using DataAccess.Domain;
    using GreenEye.DataAccess.DAO;
    using GreenEye.ViewModel.Command;
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    public class OredrManagementViewModel : BaseViewModel
    {
        //Command context menu
        public RelayCommand DeleteOrderCommand { get; set; }
        public RelayCommand NavigateEditOrderCommand { get; set; }
        public Order SelectedOrder { get; set; }
        //Phân trang thông số
        public int totalItem { get; set; } = 0;
        public int itemPerPage { get; set; } = 8;
        public int totalPage { get; set; } = 0;
        public int CurrentPage { get; set; }
        public ObservableCollection<Order> OrderPageList { get; set; }
        // Phaan trang command
        public RelayCommand NextCommand { get; set; }
        public RelayCommand PreviousCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand LastCommand { get; set; }
        //-------------------------
        public RelayCommand AddOrderNavigateCommand { get; set; }
        public ObservableCollection<Order> OrderList { get; set; }
        public NavigateStore NavigateStore { get; set; }

        //Nav
        public Order NavSelectedOrder { get; set; }
        public ObservableCollection<Order> NavOrderList { get; set; }
        public RelayCommand NavDeleteOrderCommand { get; set; }
        public RelayCommand NavNavigateEditOrderCommand { get; set; }
        public RelayCommand NavNavigateDetailOrderCommand { get; set;}

        private string _navSearchBox;
        public string  NavSearchBox { get => _navSearchBox; set
            {
                _navSearchBox = value;

               

                onPropertyChanged(nameof(NavSearchBox));

                searchNav();
            }

        }

        public void initPaging()
        {
            totalItem = OrderList.Count();

            totalPage = totalItem / itemPerPage + (totalItem % itemPerPage == 0 ? 0 : 1);
            CurrentPage = 1;
            OrderPageList = new ObservableCollection<Order>(OrderList
            .Skip((CurrentPage - 1) * itemPerPage)
            .Take(itemPerPage));
        }
        public OredrManagementViewModel(NavigateStore navigateStore)
        {
            this.NavigateStore = navigateStore;
            OrderDAO orderDAO = new OrderDAO();

            OrderList = orderDAO.getAll();

            initPaging();

            AddOrderNavigateCommand = new RelayCommand(AddOrderNavigate, null);
            NextCommand = new RelayCommand(goNext, null);
            PreviousCommand = new RelayCommand(goPrev, null);
            FirstCommand = new RelayCommand(goFirst, null);
            LastCommand = new RelayCommand(goLast, null);

            DeleteOrderCommand = new RelayCommand(deleteOrder, null);
            NavigateEditOrderCommand = new RelayCommand(navigateEditOrder, null);

            NavDeleteOrderCommand = new RelayCommand(NavDeleteOrder, null);
            NavNavigateEditOrderCommand = new RelayCommand(NavNavigateEditOrder, null);
        }

        private void NavNavigateEditOrder(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewOrderViewModel(NavigateStore, NavSelectedOrder);
        }

        private void NavDeleteOrder(object obj)
        {
            OrderDAO OrderDAO = new OrderDAO();
            Order_BookDAO order_BookDAO = new Order_BookDAO();
            RefundDAO refundDAO = new RefundDAO();


            order_BookDAO.deleteByOrder(NavSelectedOrder.OrderId);
            refundDAO.deleteByOrder(NavSelectedOrder.OrderId);


            OrderDAO.deleteOne(NavSelectedOrder);
            OrderList.Remove(NavSelectedOrder);
            
            NavOrderList.Remove(NavSelectedOrder);

            if (OrderPageList.Contains(SelectedOrder))
                OrderPageList.Remove(SelectedOrder);


        }

        private void AddOrderNavigate(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewOrderViewModel(NavigateStore);
        }

        private void navigateEditOrder(object obj)
        {
            NavigateStore.CurrentViewModel = new AddNewOrderViewModel(NavigateStore, SelectedOrder);
        }

        private void deleteOrder(object obj)
        {
            OrderDAO OrderDAO = new OrderDAO();
            Order_BookDAO order_BookDAO = new Order_BookDAO();
            RefundDAO refundDAO = new RefundDAO();


            order_BookDAO.deleteByOrder(SelectedOrder.OrderId);
            refundDAO.deleteByOrder(SelectedOrder.OrderId);


            OrderDAO.deleteOne(SelectedOrder);
            OrderList.Remove(SelectedOrder);
            if (OrderPageList.Contains(SelectedOrder))
                OrderPageList.Remove(SelectedOrder);


        }

        private void goLast(object obj)
        {
            Debug.WriteLine("-------------------------------Last-----------------------------------------");
            CurrentPage = totalPage;
            OrderPageList = new ObservableCollection<Order>(OrderList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đé trang đầu
        private void goFirst(object obj)
        {
            Debug.WriteLine("-------------------------------First-----------------------------------------");
            Debug.WriteLine(totalPage);
            Debug.WriteLine(totalItem);
            Debug.WriteLine(CurrentPage);

            CurrentPage = 1;
            OrderPageList = new ObservableCollection<Order>(OrderList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));
        }
        //Đi đến trang trước
        private void goPrev(object obj)
        {
            Debug.WriteLine("-------------------------------Prev-----------------------------------------");

            int temp = CurrentPage;
            if (temp == 1 || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp - 1);
            OrderPageList = new ObservableCollection<Order>(OrderList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Prev-----------------------------------------");
        }
        //Đi đến trang sau
        private void goNext(object obj)
        {
            Debug.WriteLine("-------------------------------Next-----------------------------------------");

            int temp = CurrentPage;
            if (temp == totalPage || totalPage == 0)
            {
                return;
            }
            CurrentPage = (temp + 1);
            OrderPageList = new ObservableCollection<Order>(OrderList.Skip((CurrentPage - 1) * itemPerPage).Take(itemPerPage));

            Debug.WriteLine("-------------------------------Next-----------------------------------------");
        }

        private void searchNav()
        {
            if (NavSearchBox != "")
            {
                string str = NavSearchBox;

                NavOrderList = new ObservableCollection<Order>();

                foreach (Order order in OrderList)
                {
                    if (str.All(char.IsDigit))
                    {
                        if (order.OrderId == Int16.Parse(str))
                            NavOrderList.Add(order);
                    }
                    else if (order.Customer.Name.Contains(str.ToLower()))
                    {
                        NavOrderList.Add(order);
                    }
                }
            }

        }
       
    }
}
