namespace Assets.ObjectManager
{
    public enum MovementType
    {
        /// <summary>
        /// Tелепортирует обекты в указанную позицию.
        /// </summary>
        Teleport,
        #region [Некинематик]
        /// <summary>
        /// [Некинематик] Толкнуть объект в заданном направлении с определенной силой.
        /// </summary>
        PushInDirection,
        #endregion
        #region [Кинематик]
        MoveToPosition,
        #endregion
    }
}
