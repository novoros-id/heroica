using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Movement speed in units per second.
    public float speed_ = 1.0F;
    // Time when the movement started.
    public float startTime;
    // Total distance between the markers.
    public float journeyLength;

    
    private List<GameObject> list_goal_1 = new List<GameObject>();
    private int count_key = 0;
    private List<GameObject> list_goal_2 = new List<GameObject>();
    private List<GameObject> list_goal_3 = new List<GameObject>();



    // Start is called before the first frame update
    void Start()

    {
        // Здесь обозначаем массивы целей

       

        list_goal_1.Add(GameObject.Find("item_key_1"));
        list_goal_1.Add(GameObject.Find("item_key"));
        list_goal_1.Add(GameObject.Find("item_key_2"));

        count_key = list_goal_1.Count;

        list_goal_2.Add(GameObject.Find("Door"));
        list_goal_2.Add(GameObject.Find("Door (2)"));
        list_goal_2.Add(GameObject.Find("Door (3)"));

        list_goal_3.Add(GameObject.Find("en_ogre_boss Variant"));

        define_goal();

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
                    if (switch_battle_move == true)
                    {
                        switch_battle_mode();
                        switch_battle_move = false;
                    }

                }
                else
                {
                    mScript.set_current_move();
                }
                // mScript.set_current_move();
                //Invoke("show_the_cube", 1.2f);
                CubeButton.SetActive(true);

            }
        }
    }

    void Update()
    {
        
    }

    private void show_the_cube()
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

    public void define_goal()
    {
        update_goal();
        float dist_to_boss = 100000;

        // сначала выбираем любой, если у нас нет ключа
        if (list_goal_1.Count != 0 && get_key() == false)
        {
            int count_goal1 = list_goal_1.Count;

            if (count_goal1 < count_key) // если ключей меньше чем вначале, идем к ближайшему
            {

                float min_distance_g1 = 10000;
                int index_min_distance_g1 = 0;

                int index_g1 = 0;
                foreach (var lg_g1 in list_goal_1)
                {
                    float dist_to_g1 = Vector3.Distance(transform.position, lg_g1.transform.position);

                    if (dist_to_g1 <= min_distance_g1)
                    {
                        min_distance_g1 = dist_to_g1;
                        index_min_distance_g1 = index_g1;
                    }

                    index_g1++;
                }

                goal = list_goal_1[index_min_distance_g1];
                return;
            }
            else // выбираем случайный
            {
                int rand_count_goal_1 = Random.Range(0, count_goal1);
                goal = list_goal_1[rand_count_goal_1];
                return;
            }
            
        }


        if (list_goal_2.Count != 0 && list_goal_3.Count != 0)
        {
            //  меряем расстояние до цели 3 - запоминаем

            dist_to_boss = Vector3.Distance(transform.position, list_goal_3[0].transform.position);

            // потом меряем расстояние до ближайшей двери
        

            float min_distance = 10000;
            int index_min_distance = 0;

            int index_ = 0;
            foreach (var lg in list_goal_2)
            {
                float dist_to_g = Vector3.Distance(transform.position, lg.transform.position);

                if (dist_to_g <= min_distance)
                {
                    min_distance = dist_to_g;
                    index_min_distance = index_;
                }

                index_++;
            }

            // если до босса  меньше, то идем к нему
            if (dist_to_boss < min_distance)
            {
                goal = list_goal_3[0];
                return;
            }
            else // если больше, то идем к ближайшей двери
            {
                goal = list_goal_2[index_min_distance];
                return;
            }
            
        }

        if (list_goal_3.Count != 0)
        {
            int count_goal3 = list_goal_3.Count;
            int rand_count_goal_3 = Random.Range(0, count_goal3);
            goal = list_goal_3[rand_count_goal_3];
            return;
        }

    }

    private void update_goal()
    {
        List<GameObject> del_index_1 = new List<GameObject>();
        List<GameObject> del_index_2 = new List<GameObject>();
        List<GameObject> del_index_3 = new List<GameObject>();

        // найдем пустые

        // int count_1 = 0;
        foreach (var lg in list_goal_1)
        {
            if (lg == null)
            {
                del_index_1.Add(lg);
            }

            // count_1++;
        }


        //int count_2 = 0;
        foreach (var lg in list_goal_2)
        {
            if (lg == null)
            {
                del_index_2.Add(lg);
            }

            //count_2++;
        }

        // int count_3 = 0;
        foreach (var lg in list_goal_3)
        {
            if (lg == null)
            {
                del_index_3.Add(lg);
            }

           //  count_2++;
        }


        // теперь удалим
        foreach (var dd in del_index_1)
        {
            list_goal_1.Remove(dd);
        }

        foreach (var dd in del_index_2)
        {
            list_goal_2.Remove(dd);
        }

        foreach (var dd in del_index_3)
        {
            list_goal_3.Remove(dd);
        }

    }


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
            mScript.add_text("У " + name + " закончились жизни. Установлен режим восстановления здоровья");
        }
        else if (leaves >= 4)
        {
            recovery_mode = false;
            leaves = 4;
            mScript.add_text("У " + name + " жизни восстановлены.");
        }
    }


    // item
    // ------------------------------
    public int get_item(string item)
    {
        if (item == "blood") return blood;
        else if(item == "luck") return luck;
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

    public void add_item(string item,int kol)
    {
        if (item == "blood")  blood += kol;
        else if (item == "luck") luck += kol;
        else if (item == "speed")  speed += kol;
        else if (item == "power")  power += kol;
        else if (item == "gold")  gold += kol;
        else if (item == "axe")  axe += kol;
        else if (item == "baton")  baton += kol;
        else if (item == "scythe")  scythe += kol;
        else if (item == "bow")  bow += kol;
        else if (item == "dagger")  dagger += kol;
        else if (item == "sword")  sword += kol;
  

    }

}
