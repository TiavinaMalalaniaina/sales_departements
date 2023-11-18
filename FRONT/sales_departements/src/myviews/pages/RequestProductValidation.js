import RequestProductValidationCard from "../cards/RequestProductValidationCard"
import { useState } from "react"


const RequestProductValidation=()=> {
    const [data, setData] = useState([
        1,2,3,4,5
    ])
    return (
        <>
            {data.map((value, index)=>
                <RequestProductValidationCard data={value} key={index}/>   
            )}
        </>
    )
}
export default RequestProductValidation