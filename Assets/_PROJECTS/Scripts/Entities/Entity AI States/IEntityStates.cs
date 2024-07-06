using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityStates 
{
    /// <summary>
    /// function to execute enemy state
    /// </summary>
    /// <param name="_entity"> Entity that going to be execute </param>
    void Execute(EntitiesCore _entity);

}
