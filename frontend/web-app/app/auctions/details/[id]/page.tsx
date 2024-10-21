import React from "react";

export default function Details({params}:{params:{id:string}}) {
    return (
        <div>
            <h1>Details for {params.id}</h1>
        </div>
    );
}