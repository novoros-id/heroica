using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public float pl_fl_x = 1.2f;
    public float pl_fl_z = 0.8f;
    private int current_move = 1;
    public GameObject[] player;
    public GameObject[] pr_hod;
    public GameObject[] floor_g;
    public GameObject selected1;

    public GameObject cam_focus;
    public bool Pc;
    public string lang = "en";
    public Text TextComment;
    public Graph g = new Graph();
    public GameObject[] Blue;

    public GameObject blue_floor;

    public void Start()
    {
        
        move_priznak_step();

        if (Application.platform == RuntimePlatform.WindowsPlayer )
        {
            Pc = true;
        }
        else
        {
            Pc = false;
        }

        build_graph();
        

    }

    /// <summary>
    /// /////// постройка графа
    /// </summary>
   

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

    public void show_the_way (string starFloor,string endFloor)
    {

        build_graph();
        clear_blue();

        var dijkstra = new Dijkstra(g);
        var path = dijkstra.FindShortestPath(starFloor, endFloor);

        int price_way = return_price_way(path);
        Debug.Log("Price way " + price_way);

        string[] subs = path.Split(',');
        foreach (var sub in subs)
        {
            //Debug.Log(sub);
            GameObject floor_ = GameObject.Find(sub);
            Instantiate(blue_floor, new Vector3(floor_.transform.position.x, 1.05f, floor_.transform.position.z), Quaternion.identity);
        }

    }

    public int return_price_way (string path)
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
                cost_way += 2;
            }
            else if (_items.tag == "Enemy_2")
            {
                cost_way += 3;
            }
            else
            {
                cost_way += 1;
            }

          
        }

        return cost_way;
    }


    /// <summary>
    /// управление ходом
    /// </summary>
   
    

    public int get_current_move()
    {
        return current_move;
    }

    public void set_current_move(string text_add = "")
    {
        // сдвинем ход 

        if (current_move == 4)
            current_move = 1;
        else
            current_move += 1;

        // передвинем знак хода
        move_priznak_step(text_add);

    }

    public void move_priznak_step(string text_add = "")
    {
       // var myText = GameObject.Find("Text_").GetComponent<Text>();

        if (text_add != "")
        {
            add_text(text_add);
        }

        // очистим значок хода

        pr_hod = GameObject.FindGameObjectsWithTag("pr_hod");
        

        for (int b = 0; b < pr_hod.Length; b++)
        {
            Destroy(pr_hod[b]);
        }

        player = GameObject.FindGameObjectsWithTag("Player");
        cam_focus = GameObject.Find("CameraFocus");

        for (int i = 0; i < player.Length; i++)
        {

            Player_ pl_script = player[i].GetComponent<Player_>();

            if (pl_script.step_move == current_move)
            {
                Instantiate(selected1, new Vector3(player[i].transform.position.x, 1.6f, player[i].transform.position.z), Quaternion.identity);
                //cam_focus.transform.position = new Vector3(player[i].transform.position.x, 0, player[i].transform.position.z);

                //text  myText;
               
                if (pl_script.get_battle_mode() == true)
                {

                    if (lang == "ru")
                    {
                        add_text("Режим боя " + player[i].name + " нажмите на кубик и узнаете исход боя");
                    }
                    else if (lang == "en")
                    {
                        add_text("Battle Mode " + player[i].name + " click on the cube and find out the outcome of the battle");
                    }
                   

                }
                else if (pl_script.recovery_mode == true)
                {
                    if (lang == "ru")
                    {
                        add_text("Режим восстановления здоровья " + player[i].name + " нажмите на кубик и будет добавлено столько здоровья, сколько выпало очков");
                    }
                    else if (lang == "en")
                    {
                        add_text("Health Recovery Mode " + player[i].name + " click on the cube and you will be added as much health as you get points");
                    }
                }
                else
                {
                    if (pl_script.comp == true)
                    {
                        if (lang == "ru")
                        {
                            add_text("Текущий ход " + player[i].name + " нажмите на кубик, затем компьютер сам сделает ход");
                        }
                        else if (lang == "en")
                        {
                            add_text("Current move " + player[i].name + " click on the cube, then the computer will make its own move");
                        } 
                    }
                    else
                    {
                        if (lang == "ru")
                        {
                            add_text("Текущий ход " + player[i].name + " нажмите на кубик, затем нажмите на вращающееся поле");
                        }
                        else if (lang == "en")
                        {
                            add_text("Current move  " + player[i].name + " click on the cube, then click on the rotating field");
                        }

                    }
                }

                // text  Text_L;
                //var l_Text = GameObject.Find("Text_L").GetComponent<Text>();
                //l_Text.text = player[i].name + " жизней:" + pl_script.get_leaves();


                break;
            }

        }
    }

    public void add_text(string a_text)
    {
        //var myText = GameObject.Find("Text_").GetComponent<Text>();
        TextComment.text = a_text + "\n" + "\n" + TextComment.text;
    }

    /// <summary>
    /// ///////////// вспомогательные
    /// </summary>

    public void clear_blue()
    {
        Blue = GameObject.FindGameObjectsWithTag("Blue");

        for (int b = 0; b < Blue.Length; b++)
        {
            Destroy(Blue[b]);
            //Debug.Log("a");
        }
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

