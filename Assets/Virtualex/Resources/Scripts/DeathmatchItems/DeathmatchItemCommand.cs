using UnityEngine;
using System.Collections;

public interface IDeathmatchItemCommand{

    void ActivateStart();
    void ActivateEnd();
    void SetHoldingController(ViveControls v);
}
