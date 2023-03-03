using System.Collections;
using System.Collections.Generic;

public class EnemyData
{
    public int EnemyId;
    public int EnemyHP;
    public List<DataBase.EnemyPattern> EnemyPatternList = new List<DataBase.EnemyPattern>();
    public int[] EnemyPatternData = new int[4];

}
