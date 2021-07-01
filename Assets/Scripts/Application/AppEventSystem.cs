namespace AppNode
{
    using AppNode.Events;

    public class AppEventSystem
    {
        public delegate void BattleSystemLoadedEventHandler(BattleSystemLoadedEventArgs eventArgs);
        public event BattleSystemLoadedEventHandler BattleSystemLoadedEvent;

        public void DispatchBattleSystemLoadedEvent()
        {
            BattleSystemLoadedEventArgs e = new BattleSystemLoadedEventArgs();
            BattleSystemLoadedEvent.Invoke(e);
        }

        #region 限制此类为单例

        private AppEventSystem() { }

        private static AppEventSystem SYSTEM = null;

        public static AppEventSystem GetInstance()
        {
            if (SYSTEM == null)
                SYSTEM = new AppEventSystem();

            return SYSTEM;
        }

        #endregion
    }

}

namespace AppNode.Events
{
    public class BattleSystemLoadedEventArgs
    {

    }
}
