import {
    CButton,
    CTable, 
    CTableBody, 
    CTableDataCell, 
    CTableHead, 
    CTableHeaderCell, 
    CTableRow,
    CFormCheck
} from '@coreui/react'

const RequestProductValidationTable = ({data}) => {
    
    return (
        <CTable striped>
            <CTableHead>
                <CTableRow>
                    <CTableHeaderCell scope="col">#</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Produit</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Quantité</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Motif</CTableHeaderCell>
                    <CTableHeaderCell scope="col">Action</CTableHeaderCell>
                </CTableRow>
            </CTableHead>
            <CTableBody>
                {data.map((value, index) => 
                <CTableRow key={index}>
                    <CTableHeaderCell scope="row">{index+1}</CTableHeaderCell>
                    <CTableDataCell>{value.productName}</CTableDataCell>
                    <CTableDataCell>{value.quantity}</CTableDataCell>
                    <CTableDataCell>{value.motif}</CTableDataCell>
                    <CTableDataCell>
                      <CFormCheck id="checkValidation" label="Validée"/>
                    </CTableDataCell>
                </CTableRow>
                )}
            </CTableBody>
        </CTable>
    )
}
export default RequestProductValidationTable