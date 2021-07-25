using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_ : MonoBehaviour
{
    public int step_move;
    public bool key = false;
    public bool battle_mode = false;
    public bool comp;

    public int leaves = 4;

    public int blood = 0;
    public int luck = 0;
    public int speed = 0;
    public int power = 0;
    public int gold = 0;
    
    public int axe = 0;
    public int baton = 0;
    public int scythe = 0;
    public int bow = 0;
    public int dagger = 0;
    public int sword = 0;

    public bool move = false;
    public Transform startMarker;
    public Vector3 endMarker;
    public bool switch_battle_move;
    private Vector3 previus_position;
    public GameObject goal;
    public bool recovery_mode = false;
    public GameObject CubeButton;

    public Graph g = new Graph();
    public GameObject[] floor_g;
// public string CurFloorName;


    // Movement speed in units per second.
    public float speed_ = 1.0F;
    // Time when the movement started.
    public float startTime;
    // Total distance between the markers.
    public float journeyLength;

    // private int count_key = 0;


    static AudioSource audiosrc;
    public AudioClip open;
    public AudioClip step;
    public AudioClip Battle_mode;
    public AudioClip Use_crystal;

    //public AudioClip HP_plus;
    public GameObject CrystalButton_;
    public GameObject CrossedSwords;
    public string CurWeapon;
   
   



    // Start is called before the first frame update
    void Start()

    {
        audiosrc = GetComponent<AudioSource>();
        set_CurWeapon();
    }

    
    public void SoundStep()
    {
        audiosrc.PlayOneShot(step);
    }

    public string get_CurWeapon()
    {
        return CurWeapon;
    }
    public string set_CurWeapon()
    {
        List<string> weapon_list = GetWeaponList();
        if(weapon_list.Count == 0)
        {
            CurWeapon = "";
            return "";
        }
        if(CurWeapon == "")
        {
            CurWeapon = weapon_list[0];
            return CurWeapon;
        }

        int CurIndexWeapon = weapon_list.IndexOf(CurWeapon);
        CurIndexWeapon++;
        if (CurIndexWeapon >= weapon_list.Count)
        {
            CurWeapon = weapon_list[0];
            return CurWeapon;
        }
        
        CurWeapon = weapon_list[CurIndexWeapon];
        return CurWeapon;
    }

    public List<string> GetWeaponList()
    {
        List<string> weapon_list = new List<string>(); 
        if(axe == 1)
        {
            weapon_list.Add("axe");
        }
        if (baton == 1)
        {
            weapon_list.Add("baton");
        }
        if (scythe == 1)
        {
            weapon_list.Add("scythe");
        }
        if (bow == 1)
        {
            weapon_list.Add("bow");
        }
        if (dagger == 1)
        {
            weapon_list.Add("dagger");
        }
        if (sword == 1)
        {
            weapon_list.Add("sword");
        }
        
        return weapon_list;
    }

    public void play_sound_use_crystal()
    {
        audiosrc.PlayOneShot(Use_crystal);
    }

    private void FixedUpdate()
    {
        if (startMarker != null)
        {


            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed_;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker, fractionOfJourney);

            if (transform.position == endMarker & move == true)
            {

                move = false;
                startMarker = null;
                transform.position = endMarker;
                // CubeButton.SetActive(true);
                // Invoke("show_the_cube", 0.5f);

                // Debug.Log("на месте " + name) ;

                GameObject cam = GameObject.Find("Directional Light");
                Main mScript = cam.GetComponent<Main>();
                if (get_battle_mode() == true)
                {
                    //switch_battle_mode();
                    mScript.move_priznak_step();
                    //audiosrc.PlayOneShot(Battle_mode);
                    if (switch_battle_move == true)
                    {
                        switch_battle_mode();
                        switch_battle_move = false;
                        

                    }
                    else
                    {
                        audiosrc.PlayOneShot(Battle_mode);
                        Instantiate(CrossedSwords, new Vector3(transform.position.x - 0.4f, 1.4f,transform.position.z), Quaternion.identity);
                    }

                }
                else
                {
                    mScript.set_current_move();
                }
                // mScript.set_current_move();
                //Invoke("show_the_cube", 1.2f);
                CubeButton.SetActive(true);
                CrystalButton_.SetActive(false);

            }
        }
    }


    public void show_the_cube()
    {
        CubeButton.SetActive(true);
        
    }

    public bool goal_live()
    {
        if (goal == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool goal_is_key()
    {
        if (goal == null)
        {
            return false;
        }
        else
        {
            if (goal.tag == "Key")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    public string return_way_to_goal ()
    {

        var dijkstra = new Dijkstra(g);

        // построим граф
        build_graph();
        string PlayerFloor = Return_floor_player(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        string GoalFloor = Return_floor_player(new Vector3(goal.transform.position.x, goal.transform.position.y, goal.transform.position.z));
        var goalway = dijkstra.FindShortestPath(PlayerFloor, GoalFloor);
        return goalway;

    }

    public string define_way()
    {

        var dijkstra = new Dijkstra(g);
        int key_price;

        // построим граф
        build_graph();
        string PlayerFloor = Return_floor_player(new Vector3(transform.position.x, transform.position.y, transform.position.z));

        GameObject boss = GameObject.Find("en_ogre_boss Variant");
        string BossFloor = Return_floor_player(new Vector3(boss.transform.position.x, boss.transform.position.y, boss.transform.position.z));
        var boss_way = dijkstra.FindShortestPath(PlayerFloor, BossFloor);
        // Debug.Log("Boss way " + boss_way);
        int boss_price = return_price_way(boss_way);
        // Debug.Log("Boss price way " + boss_price);


        // если есть ключ, то выбираем самый короткий путь до главного

        if (get_key()) {

            goal = boss;
            return boss_way;

        }

        // если нет ключа, найдем все ключи и главного персонажа
        // построим путь до ключей и до главного персонажа
        // выберем наименьший

        else
        {
            string max_way = "";
            int min_coast = 1000000;

            GameObject[] key_tag = GameObject.FindGameObjectsWithTag("Key");

            for (int b = 0; b < key_tag.Length; b++)
            {
                string keyfloor = Return_floor_player(new Vector3(key_tag[b].transform.position.x, key_tag[b].transform.position.y, key_tag[b].transform.position.z));
                var key_way = dijkstra.FindShortestPath(PlayerFloor, keyfloor);
                
                key_price = return_price_way(key_way);
                key_price = adjust_the_way_random(key_price);
                // Debug.Log("Key price " + key_price);

                if (key_price < min_coast)
                {
                    min_coast = key_price;
                    max_way = key_way;
                    goal = key_tag[b];
                }


            }

            if (min_coast < boss_price)
            {
                //Debug.Log("max way " + max_way);
                //Debug.Log("mac coast " + min_coast);
                return max_way;
            }
            else
            {
                goal = boss;
                //Debug.Log("max way " + boss_way);
                //Debug.Log("mac coast " + boss_price);
                return boss_way;
            }

        }

    }

    int adjust_the_way_random(int k_number)
    {
        //Создание объекта для генерации чисел
        // Random rnd = new Random();

        //Получить случайное число (в диапазоне от 0 до 10)
        int value_ = Random.Range(-10, 10);
        return k_number + value_;
    }

    //public void show_the_way(string starFloor, string endFloor)
    //{

    //    build_graph();
    //    clear_blue();

    //    var dijkstra = new Dijkstra(g);
    //    var path = dijkstra.FindShortestPath(starFloor, endFloor);

    //    int price_way = return_price_way(path);
    //    Debug.Log("Price way " + price_way);

    //    string[] subs = path.Split(',');
    //    foreach (var sub in subs)
    //    {
    //        //Debug.Log(sub);
    //        GameObject floor_ = GameObject.Find(sub);
    //        Instantiate(blue_floor, new Vector3(floor_.transform.position.x, 1.05f, floor_.transform.position.z), Quaternion.identity);
    //    }

    //}

    public string Return_floor_player(Vector3 pos)
    {
        GameObject[] Floors;
        float x_f = pos.x - 0;
        float z_f = pos.z + 0;
        string curlfloorname_ = "";

        Floors = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < Floors.Length; i++)
        {
            if (Mathf.Abs(Floors[i].transform.position.x - x_f) < 0.01 && Mathf.Abs(Floors[i].transform.position.z - z_f) < 0.01)
            {
                //CurFloorName = Floors[i].name;
                //Debug.Log(Floors[i].name);
                return Floors[i].name;
            }
        }

        if (curlfloorname_ == "")
        {
            return GameObject.Find("StartFloor").name;

        }

        return null;

    }


    /// <summary>
    /// Управление арсеналом игрока
    /// </summary>
  

    public bool get_comp()
    {
        return comp;
    }

    // Key
    // --------------------------------

    public bool get_key()
    {
        return key;
    }

    public void set_key()
    {
        key = true;
    }

    public void clear_key()
    {
        key = false;
        audiosrc.PlayOneShot(open);

    }

    // battle mode
    // --------------------------------

    public bool get_battle_mode()
    {
       
        return battle_mode;
    }

    public void switch_battle_mode()
    {
        if (battle_mode == true)
        {
            battle_mode = false;
        }
        else
        {
            //audiosrc.PlayOneShot(Battle_mode);
            battle_mode = true;
        }
    }

    // previus position
    // --------------------------------

    public Vector3 get_previus_position()
    {
        return previus_position;
    }

    public void set_previus_position(Vector3 pos_)
    {
        previus_position = pos_;
    }

    // leaves
    // ----------------------------------

    public int get_leaves()
    {
        return leaves;
    }

    public void add_leaves(int leaves_)

    {
        Main mScript = GameObject.Find("Directional Light").GetComponent<Main>();

        leaves += leaves_;
        if (leaves <= 0)
        {
            recovery_mode = true;
            leaves = 0;
            // mScript.add_text("У " + name + " закончились жизни. Установлен режим восстановления здоровья");
            mScript.add_text(" " + name + " lives are over. The health recovery mode is set");
        }
        else if (leaves >= 4)
        {
            recovery_mode = false;
            leaves = 4;
            // mScript.add_text("У " + name + " жизни восстановлены.");
            mScript.add_text(" " + name + " lives restored");

        }
        //audiosrc.PlayOneShot(HP_plus);
    }


    // item
    // ------------------------------
    public int get_item(string item)
    {
        if (item == "blood") return blood;
        else if (item == "luck") return luck;
        else if (item == "speed") return speed;
        else if (item == "power") return power;
        else if (item == "gold") return gold;
        else if (item == "axe") return axe;
        else if (item == "baton") return baton;
        else if (item == "scythe") return scythe;
        else if (item == "bow") return bow;
        else if (item == "dagger") return dagger;
        else if (item == "sword") return sword;

        return 0;

    }

    public void add_item(string item, int kol)
    {
        if (item == "blood") blood += kol;
        else if (item == "luck") luck += kol;
        else if (item == "speed") speed += kol;
        else if (item == "power") power += kol;
        else if (item == "gold") gold += kol;
        else if (item == "axe") axe += kol;
        else if (item == "baton") baton += kol;
        else if (item == "scythe") scythe += kol;
        else if (item == "bow") bow += kol;
        else if (item == "dagger") dagger += kol;
        else if (item == "sword") sword += kol;


    }


    public GameObject return_tag_item_on_position(Vector3 player_position)
    {
        //string tag_item = "";
        Vector3 CurFloorPos;
        Collider[] colliders;

        CurFloorPos = new Vector3(player_position.x, player_position.y, player_position.z);

        //Debug.Log("Текущая позиция платформы" + CurFloorPos);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 0.5f)).Length > 0)
        {

            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги

                if (collider.tag == "Enemy_1" ||
                    collider.tag == "Enemy_2" ||
                    collider.tag == "Enemy_boss" ||
                    collider.tag == "Key" ||
                    collider.tag == "Door" ||
                    collider.tag == "item_blood" ||
                    collider.tag == "item_luck" ||
                    collider.tag == "item_speed" ||
                    collider.tag == "item_power" ||
                    collider.tag == "item_gold" ||
                    collider.tag == "item_axe" ||
                    collider.tag == "item_baton" ||
                    collider.tag == "item_scythe" ||
                    collider.tag == "item_bow" ||
                    collider.tag == "item_dagger" ||
                    collider.tag == "item_sword")

                {

                    return collider.gameObject;


                }
            }


        }


        //string[] tag_item = new string[] { "Key", "Door", "Enemy_1","Enemy_2","Enemy_boss","item_blood"
        //                                  , "item_luck", "item_speed", "item_power", "item_gold", "item_axe", "item_baton"
        //                                  , "item_scythe", "item_bow", "item_dagger", "item_sword"};

        //foreach (string tag in tag_item)
        //{

        //    GameObject[] item = GameObject.FindGameObjectsWithTag(tag);

        //    for (int i = 0; i < item.Length; i++)
        //    {
        //        if (item[i].transform.position.x == player_position.x && item[i].transform.position.z == player_position.z)
        //        {
        //            return item[i];
        //        }
        //    }


        //}


        return null;

    }

    public void addEdgeFromFloor(string nameFloor, Graph gr)
    {

        GameObject CurFloor;
        Vector3 CurFloorPos;
        Collider[] colliders;
        float x, z;

        CurFloor = GameObject.Find(nameFloor);
        CurFloorPos = new Vector3(CurFloor.transform.position.x, CurFloor.transform.position.y, CurFloor.transform.position.z);

        if ((colliders = Physics.OverlapSphere(CurFloorPos, 1)).Length > 0)
        {
            foreach (var collider in colliders)
            {
                // обрабатываем только некоторые тэги
                if (collider.tag == "Floor")
                {

                    Vector3 pos1 = collider.transform.position - CurFloorPos;

                    // так как есть небольшие смещения, приведем к 0 округления
                    if (Mathf.Abs(pos1.x) < 0.01)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = pos1.x;
                    }

                    if (Mathf.Abs(pos1.z) < 0.01)
                    {
                        z = 0;
                    }
                    else
                    {
                        z = pos1.z;
                    }

                    // add to Graph

                    if (CurFloor.name == "StartFloor")
                    {
                        gr.AddEdge(CurFloor.name, collider.name, 1);
                    }

                    if (x > 0 && z == 0)
                    {
                        gr.AddEdge(nameFloor, collider.name, 1);
                    }

                    if (x < 0 && z == 0)
                    {
                        gr.AddEdge(nameFloor, collider.name, 1);
                    }

                    if (z > 0 && x == 0)
                    {
                        gr.AddEdge(nameFloor, collider.name, 1);
                    }

                    if (z < 0 && x == 0)
                    {
                        gr.AddEdge(nameFloor, collider.name, 1);
                    }

                }
            }
        }
    }



    public void build_graph()
    {


        //добавление вершин

        //не ставить тэг на это поле
        g.AddVertex("StartFloor");
        // остальные вершины добавляем по тэгу

        floor_g = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < floor_g.Length; i++)
        {

            g.AddVertex(floor_g[i].name);
            addEdgeFromFloor(floor_g[i].name, g);

        }

        // добавим грани
        addEdgeFromFloor("StartFloor", g);
        floor_g = GameObject.FindGameObjectsWithTag("Floor");

        for (int i = 0; i < floor_g.Length; i++)
        {

            addEdgeFromFloor(floor_g[i].name, g);

        }

    }

    public int return_price_way(string path)
    {
        int cost_way = 0;

        string[] subs = path.Split(',');
        foreach (var sub in subs)
        {
            //Debug.Log(sub);
            GameObject floor_ = GameObject.Find(sub);
            // Instantiate(blue_floor, new Vector3(floor_.transform.position.x, 1.05f, floor_.transform.position.z), Quaternion.identity);
            GameObject _items = return_tag_item_on_position(floor_.transform.position);
            if (_items == null)
            {
                cost_way += 1;
            }
            else if (_items.tag == "Door")
            {
                cost_way += 1000;
            }
            else if (_items.tag == "Enemy_1")
            {
                cost_way += 10;
            }
            else if (_items.tag == "Enemy_2")
            {
                cost_way += 10;
            }
            else
            {
                cost_way += 1;
            }


        }

        return cost_way;
    }

}

/////////////////////
/////////////////////
////////////////////
/// Graph
/// https://programm.top/c-sharp/data-structures/graph/
///

/// <summary>
/// Ребро графа
/// </summary>
public class GraphEdge
{
    /// <summary>
    /// Связанная вершина
    /// </summary>
    public GraphVertex ConnectedVertex { get; }

    /// <summary>
    /// Вес ребра
    /// </summary>
    public int EdgeWeight { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="connectedVertex">Связанная вершина</param>
    /// <param name="weight">Вес ребра</param>
    public GraphEdge(GraphVertex connectedVertex, int weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}

/// <summary>
/// Вершина графа
/// </summary>
public class GraphVertex
{
    /// <summary>
    /// Название вершины
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Список ребер
    /// </summary>
    public List<GraphEdge> Edges { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    public GraphVertex(string vertexName)
    {
        Name = vertexName;
        Edges = new List<GraphEdge>();
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="newEdge">Ребро</param>
    public void AddEdge(GraphEdge newEdge)
    {
        Edges.Add(newEdge);
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="vertex">Вершина</param>
    /// <param name="edgeWeight">Вес</param>
    public void AddEdge(GraphVertex vertex, int edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Имя вершины</returns>
    public override string ToString() => Name;
}

/// <summary>
/// Граф
/// </summary>
public class Graph
{
    /// <summary>
    /// Список вершин графа
    /// </summary>
    public List<GraphVertex> Vertices { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Graph()
    {
        Vertices = new List<GraphVertex>();
    }

    /// <summary>
    /// Добавление вершины
    /// </summary>
    /// <param name="vertexName">Имя вершины</param>
    public void AddVertex(string vertexName)
    {
        Vertices.Add(new GraphVertex(vertexName));
    }

    /// <summary>
    /// Поиск вершины
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    /// <returns>Найденная вершина</returns>
    public GraphVertex FindVertex(string vertexName)
    {
        foreach (var v in Vertices)
        {
            if (v.Name.Equals(vertexName))
            {
                return v;
            }
        }

        return null;
    }

    /// <summary>
    /// Добавление ребра
    /// </summary>
    /// <param name="firstName">Имя первой вершины</param>
    /// <param name="secondName">Имя второй вершины</param>
    /// <param name="weight">Вес ребра соединяющего вершины</param>
    public void AddEdge(string firstName, string secondName, int weight)
    {
        var v1 = FindVertex(firstName);
        var v2 = FindVertex(secondName);
        if (v2 != null && v1 != null)
        {
            v1.AddEdge(v2, weight);
            v2.AddEdge(v1, weight);
        }
    }
}


////////////////////
///////////////////
/// Алгоритм Дейкстера
///https://programm.top/c-sharp/algorithm/search/dijkstra-algorithm/
///

/// <summary>
/// Информация о вершине
/// </summary>
public class GraphVertexInfo
{
    /// <summary>
    /// Вершина
    /// </summary>
    public GraphVertex Vertex { get; set; }

    /// <summary>
    /// Не посещенная вершина
    /// </summary>
    public bool IsUnvisited { get; set; }

    /// <summary>
    /// Сумма весов ребер
    /// </summary>
    public int EdgesWeightSum { get; set; }

    /// <summary>
    /// Предыдущая вершина
    /// </summary>
    public GraphVertex PreviousVertex { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertex">Вершина</param>
    public GraphVertexInfo(GraphVertex vertex)
    {
        Vertex = vertex;
        IsUnvisited = true;
        EdgesWeightSum = int.MaxValue;
        PreviousVertex = null;
    }
}

/// <summary>
/// Алгоритм Дейкстры
/// </summary>
public class Dijkstra
{
    Graph graph;

    List<GraphVertexInfo> infos;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="graph">Граф</param>
    public Dijkstra(Graph graph)
    {
        this.graph = graph;
    }

    /// <summary>
    /// Инициализация информации
    /// </summary>
    void InitInfo()
    {
        infos = new List<GraphVertexInfo>();
        foreach (var v in graph.Vertices)
        {
            infos.Add(new GraphVertexInfo(v));
        }
    }

    /// <summary>
    /// Получение информации о вершине графа
    /// </summary>
    /// <param name="v">Вершина</param>
    /// <returns>Информация о вершине</returns>
    GraphVertexInfo GetVertexInfo(GraphVertex v)
    {
        foreach (var i in infos)
        {
            if (i.Vertex.Equals(v))
            {
                return i;
            }
        }

        return null;
    }

    /// <summary>
    /// Поиск непосещенной вершины с минимальным значением суммы
    /// </summary>
    /// <returns>Информация о вершине</returns>
    public GraphVertexInfo FindUnvisitedVertexWithMinSum()
    {
        var minValue = int.MaxValue;
        GraphVertexInfo minVertexInfo = null;
        foreach (var i in infos)
        {
            if (i.IsUnvisited && i.EdgesWeightSum < minValue)
            {
                minVertexInfo = i;
                minValue = i.EdgesWeightSum;
            }
        }

        return minVertexInfo;
    }

    /// <summary>
    /// Поиск кратчайшего пути по названиям вершин
    /// </summary>
    /// <param name="startName">Название стартовой вершины</param>
    /// <param name="finishName">Название финишной вершины</param>
    /// <returns>Кратчайший путь</returns>
    public string FindShortestPath(string startName, string finishName)
    {
        return FindShortestPath(graph.FindVertex(startName), graph.FindVertex(finishName));
    }

    /// <summary>
    /// Поиск кратчайшего пути по вершинам
    /// </summary>
    /// <param name="startVertex">Стартовая вершина</param>
    /// <param name="finishVertex">Финишная вершина</param>
    /// <returns>Кратчайший путь</returns>
    public string FindShortestPath(GraphVertex startVertex, GraphVertex finishVertex)
    {
        InitInfo();
        var first = GetVertexInfo(startVertex);
        first.EdgesWeightSum = 0;
        while (true)
        {
            var current = FindUnvisitedVertexWithMinSum();
            if (current == null)
            {
                break;
            }

            SetSumToNextVertex(current);
        }

        return GetPath(startVertex, finishVertex);
    }

    /// <summary>
    /// Вычисление суммы весов ребер для следующей вершины
    /// </summary>
    /// <param name="info">Информация о текущей вершине</param>
    void SetSumToNextVertex(GraphVertexInfo info)
    {
        info.IsUnvisited = false;
        foreach (var e in info.Vertex.Edges)
        {
            var nextInfo = GetVertexInfo(e.ConnectedVertex);
            var sum = info.EdgesWeightSum + e.EdgeWeight;
            if (sum < nextInfo.EdgesWeightSum)
            {
                nextInfo.EdgesWeightSum = sum;
                nextInfo.PreviousVertex = info.Vertex;
            }
        }
    }

    /// <summary>
    /// Формирование пути
    /// </summary>
    /// <param name="startVertex">Начальная вершина</param>
    /// <param name="endVertex">Конечная вершина</param>
    /// <returns>Путь</returns>
    string GetPath(GraphVertex startVertex, GraphVertex endVertex)
    {
        var path = endVertex.ToString();
        while (startVertex != endVertex)
        {
            endVertex = GetVertexInfo(endVertex).PreviousVertex;
            path = endVertex.ToString() + "," + path;
        }

        return path;
    }
   
}

