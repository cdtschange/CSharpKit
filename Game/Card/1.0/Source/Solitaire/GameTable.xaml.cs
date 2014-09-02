using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CdtsGame.Core.Silverlight.Card.Solitaire;
using CdtsGame.Core.Silverlight.Card;
using CdtsGame.Core.Card;

namespace Solitaire
{
    public partial class GameTable : UserControl
    {
        public GameTable()
        {
            InitializeComponent();
            this.restartButton.Click += new RoutedEventHandler(RestartButton_Click);
            this.oneCardButton.IsChecked = true;
            this.oneCardButton.Checked += new RoutedEventHandler(GameModeButton_Checked);
            this.threeCardButton.Checked += new RoutedEventHandler(GameModeButton_Checked);
            GameMode = GameModeType.OneCard;
            InitGame();
        }

        #region Properties

        /// <summary>
        /// 左上方的牌堆
        /// </summary>
        private SolitaireSourceStack SourceStack;

        /// <summary>
        /// 左上方第二个牌堆
        /// </summary>
        private SolitaireShowStack ShowStack;

        /// <summary>
        /// 右上方四个牌堆
        /// </summary>
        //private CardCompleteStack[] CompleteStackList;

        /// <summary>
        /// 下方七个牌堆
        /// </summary>
        //private CardOriginStack[] OriginStackList;

        /// <summary>
        /// 拖动的牌堆
        /// </summary>
        //private CardMoveStack MoveStack;

        /// <summary>
        /// 所有牌堆的列表(不包括拖动的)
        /// </summary>
        private SolitaireStackBase[] AllStackList;

        /// <summary>
        /// 游戏模式
        /// </summary>
        private GameModeType GameMode;

        /// <summary>
        /// 从该牌堆拖出牌
        /// </summary>
        //private CardStackBase CardStackDragFrom;

        /// <summary>
        /// 是否处于拖动
        /// </summary>
        private Boolean isDraging;

        private Point dragOldPoint;

        //private bool oddClick;

        ///// <summary>
        ///// 选中牌堆
        ///// </summary>
        //private CardMoveStack mvStack = new CardMoveStack();

        ///// <summary>
        ///// 选中的牌
        ///// </summary>
        //private CardStackBase sctStack;

        #endregion

        #region Methods

        /// <summary>
        /// 初始化游戏
        /// </summary>
        private void InitGame()
        {
            InitCardStackAndMoveStack();
           // InitOperation();
        }

        private void GameModeButton_Checked(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("确定要改变游戏模式并重新开局吗？", "确认", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                RadioButton btn = sender as RadioButton;
                if ((btn.Tag as String) == "OneCard")
                {
                    this.GameMode = GameModeType.OneCard;
                }
                else if ((btn.Tag as String) == "ThreeCard")
                {
                    this.GameMode = GameModeType.ThreeCard;
                }
                RestartGame();
            }
        }

        void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        /// <summary>
        /// 重新开始游戏
        /// </summary>
        private void RestartGame()
        {
            // 清理
            foreach (SolitaireStackBase cs in AllStackList)
            {
                cs.ReleaseToList();
                this.contentGrid.Children.Remove(cs);
            }

            InitCardStack();
        }

        /// <summary>
        /// 将1--52的数随机填入数组
        /// </summary>
        /// <returns>返回数组</returns>
        private int[] CreateRandomList()
        {
            int[] intList = new int[52];
            Random ran = new Random();

            for (int i = 1; i <= 52; i++)
            {
                int tmp = ran.Next(51);
                bool done = false;
                do
                {
                    if (intList[tmp] == 0)
                    {
                        intList[tmp] = i;
                        done = true;
                    }
                    else
                    {
                        tmp++;
                        if (tmp == 52)
                            tmp = 0;
                    }
                }
                while (done == false);
            }

            return intList;
        }

        /// <summary>
        /// 根据数值生成对应的牌
        /// </summary>
        /// <param name="v">数值</param>
        /// <returns>牌</returns>
        private Card CreateCardFromInt(int v)
        {
            int color = (v - 1) / 13;
            int value = ((v - 1) % 13) + 1;
            ulong real = (ulong)((color + 1) * 100 + value);
            CardValue cv = (CardValue)Enum.ToObject(typeof(CardValue), real);

            return new Card(cv);
        }

        /// <summary>
        /// 初始化牌堆和拖放牌堆
        /// </summary>
        private void InitCardStackAndMoveStack()
        {
            InitCardStack();
            // 实例化拖动的牌堆
            //      MoveStack = new CardMoveStack();
            //       this.DragCanvas.Children.Add(MoveStack);
        }

        /// <summary>
        /// 初始化牌堆
        /// </summary>
        private void InitCardStack()
        {
            // 创建随机数列
            int[] valueList = CreateRandomList();

            // 将数列转化成牌列
            List<Card> allCard = new List<Card>();
            foreach (int v in valueList)
                allCard.Add(CreateCardFromInt(v));

            // 左上方的牌堆有24张牌，都是背面
            Card[] cl = new Card[24];
            for (int i = 0; i < 24; i++)
            {
                cl[i] = allCard[i];
                cl[i].IsBack = true;
            }
            SourceStack = new SolitaireSourceStack(cl, GameMode);
            this.contentGrid.Children.Add(SourceStack);

            // 24张牌的牌堆右边是用来放置已经放开的牌的
            ShowStack = new SolitaireShowStack(null, GameMode);
            Grid.SetColumn(ShowStack, 1);
            this.contentGrid.Children.Add(ShowStack);

            /*          // 在右边4个是放置完成的牌的
                      CompleteStackList = new CardCompleteStack[4];
                      for (int i = 3; i < 7; i++)
                      {
                          CardCompleteStack ccs = new CardCompleteStack();
                          Grid.SetColumn(ccs, i);
                          this.ContentGrid.Children.Add(ccs);
                          CompleteStackList[i - 3] = ccs;
                      }

                      // 下方共有七组牌，分别有1，2，3，4，5，6，7张
                      OriginStackList = new CardOriginStack[7];
                      int index = 24;
                      for (int i = 0; i < 7; i++)
                      {
                          cl = new Poker[i + 1];
                          for (int j = 0; j < i + 1; j++)
                          {
                              cl[j] = allCard[index];
                              cl[j].IsBack = true;
                              index++;
                          }
                          CardOriginStack cos = new CardOriginStack(cl);
                          cos.BottomPoker.IsBack = false;
                          Grid.SetColumn(cos, i);
                          Grid.SetRow(cos, 2);
                          this.ContentGrid.Children.Add(cos);
                          OriginStackList[i] = cos;
                      }

                      // 所有牌堆加入列表
                      AllStackList = new CardStackBase[13];
                      AllStackList[0] = SourceStack;
                      AllStackList[1] = ShowStack;
                      for (int i = 0; i < 4; i++)
                      {
                          AllStackList[2 + i] = CompleteStackList[i];
                      }
                      for (int i = 0; i < 7; i++)
                      {
                          AllStackList[6 + i] = OriginStackList[i];
                      }
            */
        }
        /*
        /// <summary>
        /// 初始化操作
        /// </summary>
        private void InitOperation()
        {
            // 在LayoutRoot中接受所有鼠标事件 
            this.LayoutRoot.MouseLeftButtonDown += new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonDown);
            this.LayoutRoot.MouseLeftButtonUp += new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonUp);
            this.LayoutRoot.MouseMove += new MouseEventHandler(LayoutRoot_MouseMove);
            // 双击
            SetListen(this.LayoutRoot, new ClickManyTimesHandler(LayoutRootDoubleClick));

        }
        

        void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.oddClick = !this.oddClick;

            //if (!this.oddClick && mvStack != null)
            //{
            //    MoveStack = mvStack;
            //    isDraging = true;
            //    LayoutRoot_MouseMove(sender, e);
            //    LayoutRoot_MouseLeftButtonUp(sender, e);
            //    mvStack = null;
            //}
            //else
            //{
            CardStackBase selectStack = null;

            // 判断点在哪个牌堆上面了
            foreach (CardStackBase c in AllStackList)
            {
                Rect r = new Rect(0, 0, c.ActualWidth, c.ActualHeight);
                Point p = e.GetPosition(c);
                if (r.Contains(p))
                {
                    selectStack = c;
                    break;
                }
            }

            if (selectStack != null)
            {
                // 点在左上方的牌堆上，则翻牌
                if (selectStack == SourceStack)
                {
                    if (SourceStack.PokerCount > 0)
                    {
                        List<Poker> cl = SourceStack.SplitSourcePoker();
                        Poker[] cl2 = new Poker[cl.Count]; //倒序
                        for (int i = 0; i < cl.Count; i++)
                        {
                            cl[i].IsBack = false;
                            cl2[cl.Count - 1 - i] = cl[i];
                        }
                        ShowStack.AddShowPoker(cl2);
                    }
                    else
                    {
                        if (ShowStack.PokerCount > 0)
                        {
                            List<Poker> cl = ShowStack.ReleaseAllPoker();
                            Poker[] cl2 = new Poker[cl.Count]; //倒序
                            for (int i = 0; i < cl.Count; i++)
                            {
                                cl[i].IsBack = true;
                                cl2[cl.Count - 1 - i] = cl[i];
                            }
                            SourceStack.RefillPokerList(cl2);
                        }
                    }
                }
                else
                {
                    // 开始拖动牌
                    List<Poker> cl = new List<Poker>();
                    Point tmpP1 = new Point();
                    if (selectStack.PokerCount > 0)
                    {
                        if (selectStack is CardShowStack)
                        {
                            tmpP1 = e.GetPosition((selectStack.BottomPoker));
                            cl.Add((selectStack as CardShowStack).PopPokerFromBottom());
                        }
                        else if (selectStack is CardOriginStack)
                        {
                            tmpP1 = e.GetPosition((selectStack as CardOriginStack).SelectedPoker);
                            cl.AddRange((selectStack as CardOriginStack).SplitSelectedPoker());
                        }
                        else if (selectStack is CardCompleteStack)
                        {
                            tmpP1 = e.GetPosition((selectStack.BottomPoker));
                            cl.Add((selectStack as CardCompleteStack).PopPokerFromBottom());
                        }
                        if (cl.Count > 0)
                        {
                            this.ContentGrid.CaptureMouse();

                            CardStackDragFrom = selectStack;
                            MoveStack.AddMovePoker(cl);
                            Point tmpP2 = e.GetPosition(this.DragCanvas);
                            Point tmpP3 = new Point(tmpP2.X - tmpP1.X, tmpP2.Y - tmpP1.Y);
                            Canvas.SetLeft(MoveStack, tmpP3.X);
                            Canvas.SetTop(MoveStack, tmpP3.Y);

                            dragOldPoint = tmpP2;
                            isDraging = true;
                        }
                    }
                }
            }
            //}
        }

        void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraging)
            {
                Point np = e.GetPosition(this.DragCanvas);

                Canvas.SetLeft(MoveStack, Canvas.GetLeft(MoveStack) + np.X - dragOldPoint.X);
                Canvas.SetTop(MoveStack, Canvas.GetTop(MoveStack) + np.Y - dragOldPoint.Y);

                dragOldPoint = np;
            }
        }

        void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDraging)
            {
                CardStackBase selectStack = null;

                // 判断放在哪个牌堆上面了
                // 以鼠标为原点，判断矩形相交区域最大的
                Point movePos = e.GetPosition(MoveStack);
                Rect moveRect = new Rect(-movePos.X, -movePos.Y, MoveStack.TopPoker.ActualWidth, MoveStack.TopPoker.ActualHeight);
                Rect valueRect = new Rect(0, 0, 0, 0); // 相交的矩形
                foreach (CardStackBase c in AllStackList)
                {
                    Point stackPos = e.GetPosition(c);
                    Rect stackRect = new Rect(-stackPos.X, -stackPos.Y, c.ActualWidth, c.ActualHeight);
                    if (c.PokerCount > 0)
                    {
                        stackPos = e.GetPosition(c.BottomPoker);
                        stackRect = new Rect(-stackPos.X, -stackPos.Y, c.BottomPoker.ActualWidth, c.BottomPoker.ActualHeight);
                    }
                    stackRect.Intersect(moveRect);
                    if (stackRect.IsEmpty == false)
                    {
                        if (stackRect.Width * stackRect.Height > valueRect.Width * valueRect.Height)
                        {
                            valueRect = stackRect;
                            selectStack = c;
                        }
                    }
                }

                if (selectStack != null)
                {
                    if (selectStack is CardShowStack)
                        AddMoveCardBack();
                    else if (selectStack is CardOriginStack)
                    {
                        CardOriginStack os = selectStack as CardOriginStack;
                        if (os.CheckAddOn(MoveStack.TopPoker))
                            os.AddOriginPoker(MoveStack.ReleaseToList());
                        else
                            AddMoveCardBack();
                    }
                    else if (selectStack is CardCompleteStack)
                    {
                        if (MoveStack.PokerCount > 1)
                            AddMoveCardBack();
                        else
                        {
                            CardCompleteStack cs = selectStack as CardCompleteStack;
                            if (cs.CheckAddOn(MoveStack.TopPoker))
                                cs.AddCompletePoker(MoveStack.ReleaseToList());
                            else
                                AddMoveCardBack();
                        }
                    }
                    else if (selectStack is CardSourceStack)
                        AddMoveCardBack();
                }
                else
                {
                    AddMoveCardBack();
                }

                isDraging = false;
                this.ContentGrid.ReleaseMouseCapture();

                // 检查下方的牌堆如果有最外面一张是背面的，就把它翻过来
                foreach (CardStackBase cs in OriginStackList)
                {
                    if (cs.PokerCount > 0)
                        if (cs.BottomPoker.IsBack)
                        {
                            cs.BottomPoker.IsBack = false;

                        }
                }
            }
        }

        void LayoutRootDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CardStackBase selectStack = null;

            // 判断点在哪个牌堆上面了
            foreach (CardStackBase c in AllStackList)
            {
                Rect r = new Rect(0, 0, c.ActualWidth, c.ActualHeight);
                Point p = e.GetPosition(c);
                if (r.Contains(p))
                {
                    selectStack = c;
                    break;
                }
            }

            if (selectStack != null && selectStack.PokerCount > 0)
            {
                if (selectStack is CardOriginStack)
                {
                    Poker c = (selectStack as CardOriginStack).BottomPoker;
                    foreach (CardCompleteStack ccs in CompleteStackList)
                    {
                        if (ccs.CheckAddOn(c))
                        {
                            (selectStack as CardOriginStack).PopPokerFromBottom();
                            ccs.AddCompletePoker(c);
                            break;
                        }
                    }
                }
                else if (selectStack is CardShowStack)
                {
                    Poker c = (selectStack as CardShowStack).BottomPoker;
                    foreach (CardCompleteStack ccs in CompleteStackList)
                    {
                        if (ccs.CheckAddOn(c))
                        {
                            (selectStack as CardShowStack).PopPokerFromBottom();
                            ccs.AddCompletePoker(c);
                            break;
                        }
                    }
                }
                // 检查下方的牌堆如果有最外面一张是背面的，就把它翻过来
                foreach (CardStackBase cs in OriginStackList)
                {
                    if (cs.PokerCount > 0)
                        if (cs.BottomPoker.IsBack)
                            cs.BottomPoker.IsBack = false;
                }
            }
        }

        /// <summary>
        /// 将移动的牌还原
        /// </summary>
        private void AddMoveCardBack()
        {
            List<Poker> cl = MoveStack.ReleaseToList();
            if (CardStackDragFrom is CardShowStack)
                (CardStackDragFrom as CardShowStack).AddShowPoker(cl);
            else if (CardStackDragFrom is CardOriginStack)
                (CardStackDragFrom as CardOriginStack).AddOriginPoker(cl);
            else if (CardStackDragFrom is CardCompleteStack)
                (CardStackDragFrom as CardCompleteStack).AddCompletePoker(cl);
        }

        #region Double Click ,Three Click, Four ...

        DispatcherTimer _ClickTimer;
        int _ClickCount;
        delegate void ClickManyTimesHandler(object sender, MouseButtonEventArgs e);
        ClickManyTimesHandler _ClickManyTimesHandle;

        private void SetListen(UIElement ele, ClickManyTimesHandler handle)
        {
            ele.MouseLeftButtonUp += new MouseButtonEventHandler(ele_MouseLeftButtonUp);
            _ClickTimer = new DispatcherTimer();
            _ClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            _ClickTimer.Tick += new EventHandler(_ClickTimer_Tick);
            _ClickManyTimesHandle = handle;
        }

        void _ClickTimer_Tick(object sender, EventArgs e)
        {
            _ClickCount = 0;
            (sender as DispatcherTimer).Stop();
        }

        void ele_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _ClickCount++;
            _ClickTimer.Stop();
            _ClickTimer.Start();
            if (_ClickCount == 2)
            {
                _ClickManyTimesHandle(sender, e);
                _ClickCount = 0;
            }
        }
         * 
         * 

        #endregion
        */
        #endregion
    }
}
