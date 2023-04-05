using System.Collections;
using System.Collections.Generic;

public class PlayerQueue
{
    public class PlayerNode
    {
        private Player data;
        private PlayerNode next;

        public PlayerNode(Player data){
            this.data = data;
        }

        public Player getData(){
            return data;
        }

        public PlayerNode getNext(){
            return next;
        }

        public void setNext(PlayerNode next){
            this.next = next;
        }
    
    }

    public PlayerNode current;

    public void nextPlayer(){
        current = current.getNext();
    }

}
