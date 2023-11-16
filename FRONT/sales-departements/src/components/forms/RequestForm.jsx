export default function RequestForm({formData, addProduct, handleInputChange}) {
    
    const handleSubmit =(e)=> {
        e.preventDefault()
        const formData = new FormData(e.target)
        addProduct(formData)
    }
    
    return (
        <form onSubmit={handleSubmit}>
            <div className="row">
                <div className="col-md-12">
                    <div className="form-group">
                        <label>Produit</label>
                        <input onChange={handleInputChange} type="text" className="form-control border-input" placeholder="Produit" name="product" value={formData.product}/>
                    </div>
                    <div className="form-group">
                        <label>Quantité</label>
                        <input onChange={handleInputChange} type="text" className="form-control border-input" placeholder="Quantity" name="quantity" value={formData.quantity}/>
                    </div>
                    <div className="form-group">
                        <label>Motif</label>
                        <input onChange={handleInputChange} type="text" className="form-control border-input" placeholder="Motif" name="motif" value={formData.motif}/>
                    </div>
                </div>
            </div>
            <div className="text-center">
                <button type="submit" className="btn btn-info btn-fill btn-wd">Add</button>
            </div>
            <div className="clearfix"></div>
        </form>
    )
}