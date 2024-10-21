import React from "react";

export default function Update({params}:{params:{id:string}}) { 
    return (
        <div>
            <h1>Update for {params.id}</h1>
        </div>
    );
}