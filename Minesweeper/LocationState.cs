namespace Minesweeper
{
    public class LocationState
    {
        public bool IsRevealed { get; set; }
        public int AdjacentBombCount { get; set; }

        public string DisplayValue
        {
            get
            {
                if (!IsRevealed)
                {
                    return "?";
                }
                else if(AdjacentBombCount == 0)
                {
                    return " ";
                }
                else
                {
                    return AdjacentBombCount.ToString();
                }
            }
        }
    }
}
