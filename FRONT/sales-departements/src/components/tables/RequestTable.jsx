
import './../../styles/RequestTable.css';

export default function RequestTable({data, removeProduct, updateProduct}) {
    const handleRemove=({key})=> {
        removeProduct(key)
    }

    const handleUpdate=({key})=>{
        updateProduct(key)
    } 

    return (
    <div className="content table-responsive">
        <table className="table table-striped request-table">
            <thead>
                <tr>
                    <th>#</th>
                    <th>Produit</th>
                    <th>Quantité</th>
                    <th>Motif</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody className='request-table__body'>
                {data.map((product, index) => 
                    <tr key={index}>
                        <td className='request-table__body--id'>{index+1}</td>
                        <td className='request-table__body--product'>{product.product}</td>
                        <td className='request-table__body--quantity'>{product.quantity}</td>
                        <td className='request-table__body--quantity'>{product.motif}</td>
                        <td className='request-table__body--action'>
                            <span className="ti-pencil" onClick={handleUpdate}></span>
                            <span className="ti-close" onClick={handleRemove}></span>
                        </td> 
                    </tr>
                )}
            </tbody>
        </table>

    </div>
    )
}