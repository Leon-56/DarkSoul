using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInput {

    IEnumerator Start() {
        while(true) {
            // Dup  = 1.0f;
            // Dright = 0;
            // Jright = 1.0f;
            // Jup = 0;
            // run = true;
            // yield return new WaitForSeconds (3.0f);
            // Dup  = 0f;
            // Dright = 0;
            // Jright = 0f;
            // Jup = 0;
            // yield return new WaitForSeconds (1.0f);
            rb = true;
            yield return 0;
        }
    }


    void Update() {
        UpdateDmagDvec(Dup, Dright);
    }
}
