
const findById = (data, id) => {
    data.map(value => 
        {if (value.id === id) return value }    
    ) 
}
export default findById
    