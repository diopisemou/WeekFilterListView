using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CustomListView.Models;
using CustomListView.Views;
using CustomListView.Views.ItemTodo;

namespace CustomListView.ViewModels.ItemTodo
{
    public class ItemsTodoViewModel : BaseViewModel
    {
        public ObservableCollection<Models.ItemTodo> ItemsTodo { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command NextWeekCommand { get; set; }
        public Command PrevWeekCommand { get; set; }
        public Command WeekFromDayCommand { get; set; }
        public DateTime TimeLine { get; set; }
        public DateTime SelectedDateTimeLine { get; set; } = DateTime.Now;

        private int _weekTimeLine = 1;
        public int WeekTimeLine
        {
            get { return _weekTimeLine; }
            set { _weekTimeLine = value; OnPropertyChanged(); }
        }

        public ItemsTodoViewModel()
        {
            Title = "Browse";
            WeekTimeLine = 1;
            SelectedDateTimeLine = DateTime.Now;
            ItemsTodo = new ObservableCollection<Models.ItemTodo>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            NextWeekCommand = new Command(async () => await ExecuteNextItemsCommand(WeekTimeLine));
            PrevWeekCommand = new Command(async () => await ExecutePrevItemsCommand(WeekTimeLine));
            WeekFromDayCommand = new Command(async () => await ExecuteWeekFromDayItemsCommand(SelectedDateTimeLine));

            MessagingCenter.Subscribe<NewItemTodoPage, Models.ItemTodo>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Models.ItemTodo;
                ItemsTodo.Add(newItem);
                await ItemTodoDataStore.AddItemTodoAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItemsTodo.Clear();
                TimeLine = new DateTime();
                TimeLine = TimeLine.AddDays((WeekTimeLine * 7) -1);
                var items = await ItemTodoDataStore.GetItemsTodoAsync(TimeLine, true);
                foreach (var item in items)
                {
                    ItemsTodo.Add(item);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteNextItemsCommand(int weektosearch)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItemsTodo.Clear();
                TimeLine = new DateTime();
                TimeLine = TimeLine.AddDays(((WeekTimeLine + 1) * 7) );
                var items = await ItemTodoDataStore.GetItemsTodoAsync(TimeLine,true);
                foreach (var item in items)
                {
                    ItemsTodo.Add(item);
                }

                if (WeekTimeLine <= 1)
                {
                    WeekTimeLine = 2;
                }
                else
                {
                    WeekTimeLine = WeekTimeLine + 1;
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecutePrevItemsCommand(int weektosearch)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItemsTodo.Clear();
                TimeLine = new DateTime();
                if (WeekTimeLine <= 1)
                {
                    WeekTimeLine = 2;
                }
                TimeLine = TimeLine.AddDays(((WeekTimeLine - 1) * 7) );
                var items = await ItemTodoDataStore.GetItemsTodoAsync(TimeLine,true);
                foreach (var item in items)
                {
                    ItemsTodo.Add(item);
                }
                if (WeekTimeLine <= 1)
                {
                    WeekTimeLine = 2;
                }
                else
                {
                    WeekTimeLine = WeekTimeLine - 1;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteWeekFromDayItemsCommand(DateTime selecteddate)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItemsTodo.Clear();
                TimeLine = new DateTime();
                TimeLine = TimeLine.AddDays((selecteddate.DayOfYear) - 1);
                var items = await ItemTodoDataStore.GetItemsTodoAsync(TimeLine,true);
                foreach (var item in items)
                {
                    ItemsTodo.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}